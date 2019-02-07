using AuraServiceLib;
using RGBLighting.AuraSDKWrapper;
using RGBLighting.CUESDKWrapper;
using RGBLighting.LightControl;
using RGBLighting.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace RGBLighting {
    class Program {
        static void Main(string[] args) {
            //SetTridentZColor(255, 255, 255);

            AuraLightController auraLightController = new AuraLightController();
            foreach (AuraRgbLed led in auraLightController.Leds) {
                Console.WriteLine(led.RawLed.Name);
                led.rgb = Color.WHITE.rgb;
            }
            auraLightController.Update();

            CueLightController cueLightController = new CueLightController();
            foreach (CueRgbLed led in cueLightController.Leds) {
                led.rgb = Color.WHITE.rgb;
            }
            cueLightController.Update();

            Console.ReadLine();
        }

        public static void SetCorsairColor(int r, int g, int b) {
            CorsairProtocolDetails protocolDetails = CueSdkWrapper.PerformProtocolHandshake();
            Console.WriteLine(protocolDetails.SdkVersion);

            List<CorsairLedColor> leds = new List<CorsairLedColor>();
            for (int i = 0; i < CueSdkWrapper.GetDeviceCount(); i++) {
                Console.WriteLine(i);
                CorsairDeviceInfo deviceInfo = CueSdkWrapper.GetDeviceInfo(i);
                Console.WriteLine("type: " + deviceInfo.Type);
                Console.WriteLine(deviceInfo.Model);
                Console.WriteLine("ledsCount: " + deviceInfo.LedsCount);
                Console.WriteLine("channels: " + deviceInfo.Channels);
                Console.WriteLine("channelsCount: " + deviceInfo.Channels.ChannelsCount);

                CorsairChannelInfo[] channelArr = null;
                if (deviceInfo.Channels != null && deviceInfo.Channels.Channels != null) {
                    Console.WriteLine(deviceInfo.Channels);
                    Console.WriteLine(deviceInfo.Channels.Channels);
                    channelArr = deviceInfo.Channels.Channels;
                    Console.WriteLine("totalLedsCount: " + channelArr[0].TotalLedsCount);
                    Console.WriteLine("devicesCount: " + channelArr[0].DevicesCount);
                    CorsairChannelDeviceInfo channelDeviceInfo = channelArr[0].Devices[0];
                    Console.WriteLine("type: " + channelDeviceInfo.Type);
                    Console.WriteLine("deviceLedCount: " + channelDeviceInfo.DeviceLedCount);
                    CorsairChannelDeviceInfo channelDeviceInfo2 = channelArr[0].Devices[1];
                    Console.WriteLine("type2: " + channelDeviceInfo2.Type);
                    Console.WriteLine("deviceLedCount2: " + channelDeviceInfo2.DeviceLedCount);

                    Console.WriteLine("totalLedsCount2: " + channelArr[1].TotalLedsCount);
                    Console.WriteLine("devicesCoun2t: " + channelArr[1].DevicesCount);
                }

                switch (deviceInfo.Type) {
                    case CorsairDeviceType.CDT_LightingNodePro:
                        switch (deviceInfo.Channels.ChannelsCount) {
                            case 1:
                                for (int j = 0; j < channelArr[0].TotalLedsCount; j++) {
                                    CorsairLedColor led = new CorsairLedColor();
                                    led.ledId = CorsairLedId.CLD_C1_1 + j;
                                    led.r = r;
                                    led.g = g;
                                    led.b = b;
                                    leds.Add(led);
                                }
                                break;
                            case 2:
                                for (int j = 0; j < channelArr[1].TotalLedsCount; j++) {
                                    CorsairLedColor led = new CorsairLedColor();
                                    led.ledId = CorsairLedId.CLD_C2_1 + j;
                                    led.r = r;
                                    led.g = g;
                                    led.b = b;
                                    leds.Add(led);
                                }
                                goto case 1;
                        }
                        break;
                }
            }

            /*
            Console.WriteLine(CueSdk.GetLastError());
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CorsairLedColor)) * leds.Count);
            IntPtr currentOffset = new IntPtr(ptr.ToInt64());
            Console.WriteLine(ptr.ToInt64());
            foreach (CorsairLedColor led in leds) {
                Marshal.StructureToPtr(led, currentOffset, false);
                Console.WriteLine(currentOffset.ToInt64());
                Console.WriteLine(led.ledId);
                currentOffset = new IntPtr(currentOffset.ToInt64() + Marshal.SizeOf(typeof(CorsairLedColor)));
            }
            */
            Console.WriteLine(CueSdkWrapper.SetLedsColorsAsync(leds.Count, leds.ToArray(), IntPtr.Zero, IntPtr.Zero));

            Console.WriteLine(CueSdkWrapper.GetLastError());
        }

        public static void SetTridentZColor(int r, int g, int b) {
            IAuraSyncDeviceCollection deviceCollection = AuraSdkWrapper.BaseSdk.Enumerate(0);
            IList<string> names = new List<string>();
            IList<IAuraSyncDevice> devices = new List<IAuraSyncDevice>();
            foreach (IAuraSyncDevice device in deviceCollection) {
                bool dupe = false;
                foreach (IAuraSyncDevice toCheck in devices) {
                    if (ReferenceEquals(device, toCheck)) {
                        Console.WriteLine("found dupliacte of " + device.Name + " using ReferenceEquals");
                        dupe = true;
                    }
                    if (names.Contains(device.Name)) {
                        Console.WriteLine("found duplicate of " + device.Name + " using device.Name");
                        dupe = true;
                    }
                }
                if (!dupe) {
                    devices.Add(device);
                    names.Add(device.Name);
                    Console.WriteLine("name: " + device.Name);
                    Console.WriteLine("width: " + device.Width);
                    Console.WriteLine("height: " + device.Height);
                    foreach (IAuraRgbLight light in device.Lights) {
                        Console.WriteLine(light.Name);
                        light.Red = (byte)r;
                        light.Green = (byte)g;
                        light.Blue = (byte)b;
                    }
                    device.Apply();
                }
                Console.WriteLine(string.Join(", ", devices));
                Console.WriteLine(string.Join(", ", names));
            }
        }
    }
}
