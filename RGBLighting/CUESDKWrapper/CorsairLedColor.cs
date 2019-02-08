using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.CUESDKWrapper {
    [StructLayout(LayoutKind.Sequential)]
    public struct CorsairLedColor {
        public CorsairLedId ledId;
        public int r;
        public int g;
        public int b;

        public CorsairLedColor(CorsairLedId ledId, int r, int g, int b) {
            this.ledId = ledId;
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public CorsairLedColor(CorsairLedId ledId, int[] rgb) : this(ledId, rgb[0], rgb[1], rgb[2]) { }
        public CorsairLedColor(CorsairLedId ledId, Color color) : this(ledId, color.r, color.g, color.b) { }
    }
}
