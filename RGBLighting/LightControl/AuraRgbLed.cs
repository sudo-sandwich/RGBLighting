using AuraServiceLib;
using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public class AuraRgbLed : IRgbLed {
        public ILightController Controller { get; private set; }
        public int[] rgb {
            get => new int[] { RawLed.Red, RawLed.Green, RawLed.Blue };
            set {
                RawLed.Red = (byte)value[0];
                RawLed.Green = (byte)value[1];
                RawLed.Blue = (byte)value[2];
                Controller.UpdateRequired = true;
                Device.UpdateRequired = true;
            }
        }
        public int r {
            get => RawLed.Red;
            set {
                RawLed.Red = (byte)value;
                Controller.UpdateRequired = true;
                Device.UpdateRequired = true;
            }
        }
        public int g {
            get => RawLed.Green;
            set {
                RawLed.Green = (byte)value;
                Controller.UpdateRequired = true;
                Device.UpdateRequired = true;
            }
        }
        public int b {
            get => RawLed.Blue;
            set {
                RawLed.Blue = (byte)value;
                Controller.UpdateRequired = true;
                Device.UpdateRequired = true;
            }
        }

        public IAuraRgbLight RawLed { get; private set; }
        public AuraDevice Device { get; private set; }

        public AuraRgbLed(AuraLightController controller, IAuraRgbLight led, AuraDevice device, int r, int g, int b) {
            Controller = controller;
            RawLed = led;
            Device = device;
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public AuraRgbLed(AuraLightController controller, IAuraRgbLight led, AuraDevice device, int[] rgb) : this(controller, led, device, rgb[0], rgb[1], rgb[2]) { }
        public AuraRgbLed(AuraLightController controller, IAuraRgbLight led, AuraDevice device, Color color) : this(controller, led, device, color.r, color.g, color.b) { }
    }
}
