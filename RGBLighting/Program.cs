using AuraServiceLib;
using RGBLighting.AuraSDKWrapper;
using RGBLighting.CUESDKWrapper;
using RGBLighting.LightControl;
using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace RGBLighting {
    class Program {
        static void Main(string[] args) {

            ICollection<ILightController> controllers = new List<ILightController>() { new AuraLightController(), new CueLightController() };

            Stopwatch stopwatch = new Stopwatch();
            int i = 0;
            while (true) {
                stopwatch.Start();
                Color color = RGBRainbow.GetColor(i);

                foreach (ILightController controller in controllers) {
                    foreach (IRgbLed led in controller.Leds) {
                        led.rgb = color.rgb;
                    }
                    controller.Update();
                }

                stopwatch.Stop();
                long ellapsed = stopwatch.ElapsedMilliseconds;
                int timeToWait = 25 - (int)ellapsed;
                //Console.WriteLine("took " + ellapsed + " milliseconds, wating " + timeToWait + " more milliseconds");
                stopwatch.Reset();
                i += 5;
                i %= RGBRainbow.LENGTH;
                Thread.Sleep(timeToWait > 0 ? timeToWait : 0);
            }
        }
    }
}
