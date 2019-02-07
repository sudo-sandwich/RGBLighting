using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.Util {
    public class RGBRainbow {
        public byte Red {
            get {
                return rgb[0];
            }
        }
        public byte Green {
            get {
                return rgb[1];
            }
        }
        public byte Blue {
            get {
                return rgb[2];
            }
        }

        private int CurrentColor { get; set; } = 0;
        private int NextColor { get {
                return (CurrentColor + 1) % 3;
            }
        }

        private byte[] rgb = new byte[3] { 255, 0, 0 }; //rgb[0] = red, rgb[1] = green, rgb[2] = blue
        private bool increase = true; //whether or not the rainbow is currently increasing a color

        public void Next(int amount) {
            if (increase && rgb[NextColor] + amount > 255) {
                amount -= 255 - rgb[NextColor];
                rgb[NextColor] = 255;
                increase = false;
            } else if (!increase && rgb[CurrentColor] - amount < 0) {
                amount -= rgb[CurrentColor];
                rgb[CurrentColor] = 0;
                increase = true;
                CurrentColor = NextColor;
            }

            while (amount > 255) {
                amount -= 255;
                if (increase) {
                    rgb[NextColor] = 255;
                    increase = false;
                } else {
                    rgb[CurrentColor] = 0;
                    increase = true;
                    CurrentColor = NextColor;
                }
            }

            if (increase) {
                rgb[NextColor] = (byte)(rgb[NextColor] + amount);
            } else {
                rgb[CurrentColor] = (byte)(rgb[CurrentColor] - amount);
            }
        }

        //advances as much as possible, setting each rgb to either 0 or 255, stopping when a full shift cannot be made, returning the amount left. assumes all rgb values are either 0 or 255.
        private int AdvanceMax(int amount) {
            while (amount >= 255) {
                amount -= 255;
                if (increase) {
                    rgb[NextColor] = 255;
                } else {
                    rgb[CurrentColor] = 0;
                    CurrentColor = NextColor;
                }
                increase = !increase;
            }
            return amount;
        }

        public override string ToString() {
            return "(" + Red + ", " + Green + ", " + Blue + ")";
        }
    }
}
