using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public interface ILightController<TController, TLed> where TController : ILightController<TController, TLed> where TLed : IRgbLed<TController, TLed> {
        TLed[] Leds { get; }
        bool UpdateRequired { get; set; }

        void Update();
    }
}
