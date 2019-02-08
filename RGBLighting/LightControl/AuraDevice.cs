using AuraServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public class AuraDevice {
        public IAuraSyncDevice RawDevice { get; private set; }
        //public IList<AuraRgbLed> Leds { get; private set; }
        public bool UpdateRequired { get; set; }

        public AuraDevice(IAuraSyncDevice device) {
            RawDevice = device;
            UpdateRequired = true;
        }

        public void Update() {
            if (UpdateRequired) {
                RawDevice.Apply();
                UpdateRequired = false;
            }
        }
    }
}
