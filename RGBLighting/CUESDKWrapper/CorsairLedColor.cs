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
        //TODO: add a constructor
    }
}
