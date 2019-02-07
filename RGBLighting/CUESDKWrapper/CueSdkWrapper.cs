using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.CUESDKWrapper {
    public class CueSdkWrapper {
        public static CorsairProtocolDetails ProtocolDetails { get; private set; }

        static CueSdkWrapper() {
            ProtocolDetails = PerformProtocolHandshake();
        }

        [DllImport("CUESDK.x64_2015.dll", EntryPoint = "CorsairSetLedsColors", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool _SetLedsColors(int size, CorsairLedColor[] ledsColors);

        [DllImport("CUESDK.x64_2015.dll", EntryPoint = "CorsairSetLedsColorsAsync", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetLedsColorsAsync(int size, CorsairLedColor[] ledsColors, IntPtr callbackType, IntPtr context);
        public static bool SetLedsColorsAsync(int size, CorsairLedColor[] ledsColors) {
            return SetLedsColorsAsync(size, ledsColors, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("CUESDK.x64_2015.dll", EntryPoint = "CorsairGetDeviceCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDeviceCount();

        [DllImport("CUESDK.x64_2015.dll", EntryPoint = "CorsairGetDeviceInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetDeviceInfo(int deviceIndex);
        public static CorsairDeviceInfo GetDeviceInfo(int deviceIndex) {
            return new CorsairDeviceInfo(Marshal.PtrToStructure<_CorsairDeviceInfo>(_GetDeviceInfo(deviceIndex)));
        }

        [DllImport("CUESDK.x64_2015.dll", EntryPoint = "CorsairPerformProtocolHandshake", CallingConvention = CallingConvention.Cdecl)]
        public static extern _CorsairProtocolDetails _PerformProtocolHandshake();
        public static CorsairProtocolDetails PerformProtocolHandshake() {
            return new CorsairProtocolDetails(_PerformProtocolHandshake());
        }

        [DllImport("CUESDK.x64_2015.dll", EntryPoint = "CorsairGetLastError", CallingConvention = CallingConvention.Cdecl)]
        public static extern CorsairError GetLastError();
    }
}
