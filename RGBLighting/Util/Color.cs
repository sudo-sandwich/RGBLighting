using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.Util {
    public struct Color {
        //colors range from 0 - 255
        public int[] rgb { get; private set; }
        public int r { get => rgb[0]; }
        public int g { get => rgb[1]; }
        public int b { get => rgb[2]; }

        public Color(int r, int g, int b) {
            rgb = new int[3];
            rgb[0] = r;
            rgb[1] = g;
            rgb[2] = b;
        }

        public Color(int[] rgb) {
            this.rgb = rgb;
        }

        public override string ToString() {
            return "(" + r + ", " + g + ", " + b + ")";
        }

        //lerps from one color to another. distance will be clamped between 0 and 1.
        public static Color Lerp(Color start, Color end, float distance) {
            distance = MathUtil.Clamp(distance, 0, 1);
            int[] newRgb = new int[3];
            for (int i = 0; i < 3; i++) {
                newRgb[i] = MathUtil.LerpInt(start.rgb[i], end.rgb[i], distance);
            }
            return new Color(newRgb);
        }

        public static readonly Color BLACK = new Color(0, 0, 0);
        public static readonly Color WHITE = new Color(255, 255, 255);
        public static readonly Color RED = new Color(255, 0, 0);
        public static readonly Color GREEN = new Color(0, 255, 0);
        public static readonly Color BLUE = new Color(0, 0, 255);
    }
}
