using Unity.Burst;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.Util {

    [BurstCompile]
    public static class MathUtil {
        
        public static float2 PointInsideRadiusCircle(ref Random random, float radius) {
            // generate random alpha value [0-2π]
            float a = 2 * math.PI * random.NextFloat();
            float r = radius * math.sqrt(random.NextFloat());
            float x = r * math.cos(a);
            float y = r * math.sin(a);

            return new float2(x, y);
        }

        public static float3 PointOnUnitSphere(ref Random random) {

            float a = 2 * math.PI * random.NextFloat();
            float b = math.acos(1 - 2 * random.NextFloat());
            
            float x = math.sin(b) * math.cos(a);
            float y = math.sin(b) * math.sin(a);
            float z = math.cos(b);
            
            return new float3(x, y, z);
        }
    }

}