using RGBLighting.CUESDKWrapper;
using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.LightControl {
    public class CueLightController : ILightController {
        public IRgbLed[] Leds { get; private set; }
        public bool UpdateRequired { get; set; }
        
        public CorsairDeviceInfo[] Devices { get; private set; }

        public CueLightController() {
            UpdateRequired = true;

            ICollection<CueRgbLed> leds = new List<CueRgbLed>();
            
            Devices = new CorsairDeviceInfo[CueSdkWrapper.GetDeviceCount()];
            for (int i = 0; i < Devices.Length; i++) {
                Devices[i] = CueSdkWrapper.GetDeviceInfo(i);
                switch (Devices[i].Type) {
                    case CorsairDeviceType.CDT_LightingNodePro:
                        switch (Devices[i].Channels.ChannelsCount) {
                            case 1:
                                for (int j = 0; j < Devices[i].Channels.Channels[0].TotalLedsCount; j++) {
                                    leds.Add(new CueRgbLed(this, CorsairLedId.CLD_C1_1 + j, Color.BLACK));
                                }
                                break;
                            case 2:
                                for (int j = 0; j < Devices[i].Channels.Channels[1].TotalLedsCount; j++) {
                                    leds.Add(new CueRgbLed(this, CorsairLedId.CLD_C2_1 + j, Color.BLACK));
                                }
                                goto case 1;
                        }
                        break;
                }
            }
            Leds = leds.ToArray();
        }

        public void Update() {
            if (UpdateRequired) {
                CorsairLedColor[] corsairLedColors = new CorsairLedColor[Leds.Length];
                for (int i = 0; i < Leds.Length; i++) {
                    corsairLedColors[i] = ((CueRgbLed)Leds[i]).RawLed;
                }
                CueSdkWrapper.SetLedsColorsAsync(corsairLedColors.Length, corsairLedColors);
                //CueSdkWrapper.SetLedsColors(corsairLedColors.Length, corsairLedColors);
            }
        }
    }
}
