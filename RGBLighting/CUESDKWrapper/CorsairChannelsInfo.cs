using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.CUESDKWrapper {
    [StructLayout(LayoutKind.Sequential)]
    public struct _CorsairChannelsInfo {
        public int channelsCount;
        public IntPtr channels; //array of _CorsairChannelsInfo
    }

    public class CorsairChannelsInfo {
        public _CorsairChannelsInfo Data { get; private set; }

        public int ChannelsCount { get => Data.channelsCount; }
        public CorsairChannelInfo[] Channels { get; private set; }

        public CorsairChannelsInfo(_CorsairChannelsInfo data) {
            Data = data;

            if (Data.channels != IntPtr.Zero) {
                Channels = new CorsairChannelInfo[ChannelsCount];
                int structSize = Marshal.SizeOf(typeof(_CorsairChannelInfo));
                for (int i = 0; i < ChannelsCount; i++) {
                    Channels[i] = new CorsairChannelInfo(Marshal.PtrToStructure<_CorsairChannelInfo>(IntPtr.Add(Data.channels, structSize)));
                }
            } else {
                Channels = null;
            }
        }
    }
}
