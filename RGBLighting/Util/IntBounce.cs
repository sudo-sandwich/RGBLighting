using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.Util {
    public class IntBounce {
        public int Value { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        private bool Direction { get; set; } //true for positive, false for negative

        public int Length {
            get {
                return Max - Min;
            }
        }

        public IntBounce(int startValue, int min, int max, bool direction = true) {
            Value = startValue;
            Min = min;
            Max = max;
            Direction = direction;
        }

        public int Next(int amount) {
            if (Direction && Value + amount > Max) {
                amount -= Max - Value;
                Value = Max;
                Direction = false;
                amount = BounceMinMax(amount);
            } else if (!Direction && Value - amount < Min) {
                amount -= Value - Min;
                Value = Min;
                Direction = true;
                amount = BounceMinMax(amount);
            }
            Value += Direction ? amount : -amount;
            return Value;
        }

        //bounces from the min and max, stopping when a full bounce from Min to Max (or vice versa) cannot be made, returning the amount left. assumes the current value is either min (if Direction is true) or max (if Direction is false)
        private int BounceMinMax(int amount) {
            while (amount >= Length) {
                amount -= Length;
                Direction = !Direction;
            }
            Value = Direction ? Min : Max;
            return amount;
        }
    }
}
