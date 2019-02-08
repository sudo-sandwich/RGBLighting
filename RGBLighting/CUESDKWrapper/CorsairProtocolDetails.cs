using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.CUESDKWrapper {
    [StructLayout(LayoutKind.Sequential)]
    public struct _CorsairProtocolDetails {
        public IntPtr sdkVersion;
        public IntPtr serverVersion;
        public int sdkProtocolVersion;
        public int serverProtocolVersion;
        public byte breakingChanges;
    }

    public class CorsairProtocolDetails {
        public _CorsairProtocolDetails Data;

        public string SdkVersion { get; private set; }
        public string ServerVersion { get; private set; }
        public int SdkProtocolVersion { get => Data.sdkProtocolVersion; }
        public int ServerProtocolVersion { get => Data.serverProtocolVersion; }
        public byte BreakingChanges { get => Data.breakingChanges; }

        public CorsairProtocolDetails(_CorsairProtocolDetails data) {
            Data = data;
            SdkVersion = Marshal.PtrToStringAnsi(Data.sdkVersion);
            ServerVersion = Marshal.PtrToStringAnsi(Data.serverVersion);
        }
    }
}
