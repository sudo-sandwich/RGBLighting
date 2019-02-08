using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.Util {
    public static class RGBRainbow {
        public const int COLOR_OFFSET = 512;
        public const int LENGTH = COLOR_OFFSET * 3;
        private readonly static IntPath path;
        static RGBRainbow() {
            path = new IntPath(new List<IntPath.IntPathPoint>() {
                new IntPath.IntPathPoint(255, 256),
                new IntPath.IntPathPoint(255, 256),
                new IntPath.IntPathPoint(0, 256),
                new IntPath.IntPathPoint(0, 256)
            });
            path.BouncePath();
        }
        
        public static Color GetColor(int location) {
            return new Color(path.GetValue(location % LENGTH), path.GetValue((location + 2 * COLOR_OFFSET) % LENGTH), path.GetValue((location + COLOR_OFFSET) % LENGTH));
        }
    }
}
