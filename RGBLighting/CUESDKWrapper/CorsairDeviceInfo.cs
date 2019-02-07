using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.CUESDKWrapper {
    [StructLayout(LayoutKind.Sequential)]
    public struct _CorsairDeviceInfo {
        public CorsairDeviceType type;
        public IntPtr model;
        public CorsairPhysicalLayout physicalLayout;
        public CorsairLogicalLayout logicalLayout;
        public int capsMask;
        public int ledsCount;
        public _CorsairChannelsInfo channels;
    }

    public class CorsairDeviceInfo {
        public _CorsairDeviceInfo Data { get; private set; }

        public CorsairDeviceType Type { get => Data.type; }
        public string Model { get; private set; }
        public CorsairPhysicalLayout PhysicalLayout { get => Data.physicalLayout; }
        public CorsairLogicalLayout LogicalLayout { get => Data.logicalLayout; }
        public int CapsMask { get => Data.capsMask; }
        public int LedsCount { get => Data.ledsCount; }
        public CorsairChannelsInfo Channels { get; private set; }

        public CorsairDeviceInfo(_CorsairDeviceInfo data) {
            Data = data;
            Model = Marshal.PtrToStringAnsi(Data.model);
            Channels = new CorsairChannelsInfo(Data.channels);
        }
    }
}
