using RGBLighting.CUESDKWrapper;
using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public class CueRgbLed : IRgbLed {
        public ILightController Controller { get; private set; }

        public int[] rgb {
            get {
                return new int[] { rawLed.r, rawLed.g, rawLed.b };
            }
            set {
                rawLed.r = value[0];
                rawLed.g = value[1];
                rawLed.b = value[2];
                Controller.UpdateRequired = true;
            }
        }
        public int r {
            get => rawLed.r;
            set {
                rawLed.r = value;
                Controller.UpdateRequired = true;
            }
        }
        public int g {
            get => rawLed.g;
            set {
                rawLed.g = value;
                Controller.UpdateRequired = true;
            }
        }
        public int b {
            get => rawLed.b;
            set {
                rawLed.b = value;
                Controller.UpdateRequired = true;
            }
        }

        private CorsairLedColor rawLed;
        public CorsairLedColor RawLed { get => rawLed; }

        public CueRgbLed(CueLightController controller, CorsairLedId ledId, int r, int g, int b) {
            Controller = controller;
            rawLed = new CorsairLedColor(ledId, r, g, b);
        }
        public CueRgbLed(CueLightController controller, CorsairLedId ledId, int[] rgb) : this(controller, ledId, rgb[0], rgb[1], rgb[2]) { }
        public CueRgbLed(CueLightController controller, CorsairLedId ledId, Color color) : this(controller, ledId, color.r, color.g, color.b) { }
    }
}
