using AuraServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.AuraSDKWrapper {
    public static class AuraSdkWrapper {
        public static AuraSdk BaseSdk { get; private set; } = new AuraSdk();
        public static bool HasControl { get; private set; } = false;

        static AuraSdkWrapper() {
            SetControl(true);
        }

        //only flips control if the desired control is different from the current control
        public static void SetControl(bool control) {
            if (HasControl != control) {
                HasControl = !HasControl;
                BaseSdk.SwitchMode();
            }
        }
        
        public static IAuraSyncDeviceCollection GetEnumerator() {
            return BaseSdk.Enumerate(0);
        }
    }
}
