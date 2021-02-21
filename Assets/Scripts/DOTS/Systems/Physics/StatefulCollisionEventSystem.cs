using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Physics {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StepPhysicsWorld))]
    [UpdateBefore(typeof(EndFramePhysicsSystem))]
    public class StatefulCollisionEventSystem : SystemBase {

        private StepPhysicsWorld _stepPhysicsWorld;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private EndFramePhysicsSystem _endFramePhysicsSystem;
        
        private EntityQuery _collisionBufferQuery;

        private NativeList<StatefulCollisionEvent> _previousFrameCollisionEvents;
        private NativeList<StatefulCollisionEvent> _currentFrameCollisionEvents;

        protected override void OnCreate() {
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _endFramePhysicsSystem = World.GetOrCreateSystem<EndFramePhysicsSystem>();

            // create persistant lists of collision events that have occured within the world
            _previousFrameCollisionEvents = new NativeList<StatefulCollisionEvent>(Allocator.Persistent);
            _currentFrameCollisionEvents = new NativeList<StatefulCollisionEvent>(Allocator.Persistent);

            // query to find any entity with a CollisionEventBuffer
            var queryDesc = new EntityQueryDesc() {
                All = new ComponentType[] {
                    typeof(StatefulCollisionEventBuffer)
                }
            };
            _collisionBufferQuery = GetEntityQuery(queryDesc);
        }

        protected override void OnUpdate() {

            // check if any entities with Collision buffers exist
            if (_collisionBufferQuery.CalculateEntityCount() == 0) {
                return;
            }
            
            Dependency = JobHandle.CombineDependencies(_stepPhysicsWorld.FinalSimulationJobHandle, Dependency);

            // clear the collision event buffer of any past collisions 
            Entities
                .WithName("CleanCollisionEventBuffers")
                .WithBurst()
                .WithAny<StatefulCollisionEventBuffer>()
                .ForEach((Entity entity, ref DynamicBuffer<StatefulCollisionEvent> collisionBuffer) => {
                    collisionBuffer.Clear();
                }).ScheduleParallel();

            SwapCollisionEventStates();

            var previousFrameCollisionEvents = _previousFrameCollisionEvents;
            var currentFrameCollisionEvents = _currentFrameCollisionEvents;

            var collisionEventBuffer = GetBufferFromEntity<StatefulCollisionEvent>();
            var collisionEventBufferTag = GetComponentDataFromEntity<StatefulCollisionEventBuffer>();

            var entitiesWithBufferSet = new NativeHashSet<Entity>(0, Allocator.TempJob);
            
            Entities
                .WithName("GetEntitiesWithCollisionBuffer")
                .WithBurst()
                .WithAll<StatefulCollisionEventBuffer>()
                .ForEach((Entity entity, ref DynamicBuffer<StatefulCollisionEvent> collisionBuffer) => {
                    entitiesWithBufferSet.Add(entity);
                }).Schedule();

            var collisionEventCollectionJob = new CollisionEventCollectionJob() {
                statefulCollisionEvents = currentFrameCollisionEvents,
                collisionEventBuffers = collisionEventBufferTag,
                entitiesWithCollisionBuffers = entitiesWithBufferSet,
                physicsWorld = _buildPhysicsWorld.PhysicsWorld
            };

            Dependency = collisionEventCollectionJob.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, Dependency);

            Job
                .WithName("DistributeStatefulCollisionEventsToEntities")
                .WithBurst()
                .WithCode(() => {
                    currentFrameCollisionEvents.Sort();

                    var collisionEventStates = new NativeList<StatefulCollisionEvent>(currentFrameCollisionEvents.Length, Allocator.Temp);
                    UpdateCollisionEventStates(previousFrameCollisionEvents, currentFrameCollisionEvents, collisionEventStates);
                    DistributeCollisionEventsToBuffers(ref collisionEventBuffer, entitiesWithBufferSet, collisionEventStates);
                }).Schedule();
            
            _endFramePhysicsSystem.AddInputDependency(Dependency);
            entitiesWithBufferSet.Dispose(Dependency);
        }
        
        protected override void OnDestroy() {
            _previousFrameCollisionEvents.Dispose();
            _currentFrameCollisionEvents.Dispose();
        }
        
        private void SwapCollisionEventStates() {
            var _ = _previousFrameCollisionEvents;
            _previousFrameCollisionEvents = _currentFrameCollisionEvents;
            _currentFrameCollisionEvents = _;
            _currentFrameCollisionEvents.Clear();
        }

        private static void DistributeCollisionEventsToBuffers(ref BufferFromEntity<StatefulCollisionEvent> collisionEventBuffer, NativeHashSet<Entity> entitiesWithBufferSet, NativeList<StatefulCollisionEvent> collisionEventStates) {
            for (int i = 0; i < collisionEventStates.Length; i++) {
                var statefulCollisionEvent = collisionEventStates[i];
                if (entitiesWithBufferSet.Contains(statefulCollisionEvent.EntityA)) {
                    collisionEventBuffer[statefulCollisionEvent.EntityA].Add(statefulCollisionEvent);
                }
                if (entitiesWithBufferSet.Contains(statefulCollisionEvent.EntityB)) {
                    collisionEventBuffer[statefulCollisionEvent.EntityB].Add(statefulCollisionEvent);
                }
            }
        }

        private static void UpdateCollisionEventStates(NativeList<StatefulCollisionEvent> previousFrameCollisionEvents, NativeList<StatefulCollisionEvent> currentFrameCollisionEvents, NativeList<StatefulCollisionEvent> resultingStates) {
            int i = 0;
            int j = 0;

            while (i < currentFrameCollisionEvents.Length && j < previousFrameCollisionEvents.Length) {
                var currentFrameCollisionEvent = currentFrameCollisionEvents[i];
                var previousFrameCollisionEvent = previousFrameCollisionEvents[j];

                // check if any change occurred 
                int result = currentFrameCollisionEvent.CompareTo(previousFrameCollisionEvent);
                
                // check result to determine collision state
                if (result == 0) {
                    currentFrameCollisionEvent.CollisionState = CollisionEventState.Stay;
                    resultingStates.Add(currentFrameCollisionEvent);
                    i++;
                    j++;
                }
                else if (result < 0) {
                    currentFrameCollisionEvent.CollisionState = CollisionEventState.Enter;
                    resultingStates.Add(currentFrameCollisionEvent);
                    i++;
                }
                else {
                    previousFrameCollisionEvent.CollisionState = CollisionEventState.Exit;
                    resultingStates.Add(previousFrameCollisionEvent);
                    j++;
                }
            }

            if (i == currentFrameCollisionEvents.Length) {
                while (j < previousFrameCollisionEvents.Length) {
                    var collisionEvent = previousFrameCollisionEvents[j++];
                    collisionEvent.CollisionState = CollisionEventState.Exit;
                    resultingStates.Add(collisionEvent);
                }
            }
            else if (j == previousFrameCollisionEvents.Length) {
                while (i < currentFrameCollisionEvents.Length) {
                    var collisionEvent = currentFrameCollisionEvents[i++];
                    collisionEvent.CollisionState = CollisionEventState.Enter;
                    resultingStates.Add(collisionEvent);
                }
            }
        }
    }

}