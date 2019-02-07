using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.Util {
    public static class Math {
        public static T Clamp<T>(T value, T min, T max) where T : IComparable {
            if (value.CompareTo(min) < 0) {
                return min;
            } else if (value.CompareTo(max) > 0) {
                return max;
            } else {
                return value;
            }
        }

        //lerps an int between start and end. distance will be clamped between 0 and 1.
        public static int LerpInt(int start, int end, float distance) {
            distance = Clamp(distance, 0, 1);
            return (int)(start + (end - start) * distance);
        }
    }
}
