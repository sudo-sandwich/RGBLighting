using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public interface IRgbLed {
        ILightController Controller { get; }
        int[] rgb { get; set; }
        int r { get; set; }
        int g { get; set; }
        int b { get; set; }
    }
}
