using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.CUESDKWrapper {
    [StructLayout(LayoutKind.Sequential)]
    public struct _CorsairChannelInfo {
        public int totalLedsCount;
        public int devicesCount;
        public IntPtr devices; //array of _CorsairChannelDeviceInfo
    }

    public class CorsairChannelInfo {
        public _CorsairChannelInfo Data { get; private set; }

        public int TotalLedsCount { get => Data.totalLedsCount; }
        public int DevicesCount { get => Data.devicesCount; }
        public CorsairChannelDeviceInfo[] Devices { get; private set; }

        public CorsairChannelInfo(_CorsairChannelInfo data) {
            Data = data;

            if (Data.devices != IntPtr.Zero) {
                Devices = new CorsairChannelDeviceInfo[DevicesCount];
                int structSize = Marshal.SizeOf(typeof(_CorsairChannelDeviceInfo));
                for (int i = 0; i < DevicesCount; i++) {
                    Devices[i] = new CorsairChannelDeviceInfo(Marshal.PtrToStructure<_CorsairChannelDeviceInfo>(IntPtr.Add(Data.devices, structSize)));
                }
            } else {
                Devices = null;
            }
        }
    }
}
