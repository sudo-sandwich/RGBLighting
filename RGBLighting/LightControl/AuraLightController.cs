using AuraServiceLib;
using RGBLighting.AuraSDKWrapper;
using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public class AuraLightController : ILightController<AuraLightController, AuraRgbLed> {
        public AuraRgbLed[] Leds { get; private set; }
        public bool UpdateRequired { get; set; }

        public AuraDevice[] Devices { get; private set; }

        public AuraLightController() {
            UpdateRequired = true;

            ICollection<AuraRgbLed> leds = new List<AuraRgbLed>();
            ICollection<AuraDevice> devices = new List<AuraDevice>();
            foreach (IAuraSyncDevice rawDevice in AuraSdkWrapper.GetEnumerator()) {
                bool duplicate = false;
                foreach (AuraDevice wrappedDevice in devices) {
                    //have to check for names because Aura SDK sometimes returns duplicates of the same device, and comparing reference equality doesnt work to identify duplicates
                    if (wrappedDevice.RawDevice.Name == rawDevice.Name) {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate) {
                    AuraDevice newWrappedDevice = new AuraDevice(rawDevice);
                    devices.Add(newWrappedDevice);
                    foreach(IAuraRgbLight rawLed in rawDevice.Lights) {
                        AuraRgbLed newWrappedLed = new AuraRgbLed(this, rawLed, newWrappedDevice, Color.BLACK);
                        newWrappedDevice.Leds.Add(newWrappedLed);
                        leds.Add(newWrappedLed);
                    }
                }
            }
            Leds = leds.ToArray();
            Devices = devices.ToArray();
        }

        public void Update() {
            if (UpdateRequired) {
                foreach (AuraDevice device in Devices) {
                    device.Update();
                }
                UpdateRequired = false;
            }
        }
    }
}
