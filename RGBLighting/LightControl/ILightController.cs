using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public interface ILightController {
        IRgbLed[] Leds { get; }
        bool UpdateRequired { get; set; }

        void Update();
    }
}
