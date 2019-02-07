using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.CUESDKWrapper {
    [StructLayout(LayoutKind.Sequential)]
    public struct _CorsairChannelDeviceInfo {
        public CorsairChannelDeviceType type;
        public int deviceLedCount;
    }

    public class CorsairChannelDeviceInfo {
        public _CorsairChannelDeviceInfo Data { get; private set; }

        public CorsairChannelDeviceType Type { get => Data.type; }
        public int DeviceLedCount { get => Data.deviceLedCount; }

        public CorsairChannelDeviceInfo(_CorsairChannelDeviceInfo data) {
            Data = data;
        }
    }
}
