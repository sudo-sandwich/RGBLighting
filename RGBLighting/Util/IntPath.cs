using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.Util {
    public class IntPath {
        public struct IntPathPoint {
            public readonly int value;
            public readonly int length; //distance to the next point
            public IntPathPoint(int value, int length) {
                this.value = value;
                this.length = length;
            }
            public override string ToString() {
                return "(" + value + ", " + length + ")";
            }
        }

        public IList<IntPathPoint> Points { get; set; } //length of last point is always ignored

        public int Length {
            get {
                int len = 0;
                //ignore the length of the last point
                for (int i = 0; i < Points.Count - 1; i++) {
                    len += Points[i].length;
                }
                return len;
            }
        }

        public IntPath() {
            Points = new List<IntPathPoint>();
        }
        public IntPath(IList<IntPathPoint> points) {
            Points = points;
        }

        //bounces the path, concatenating a reversed version of the path to the current path
        public void BouncePath() {
            IList<IntPathPoint> pathNormal = new List<IntPathPoint>(Points);
            IList<IntPathPoint> pathReversed = new List<IntPathPoint>(Points);
            pathReversed = pathReversed.Reverse().ToList();
            pathReversed.RemoveAt(0);
            pathNormal[pathNormal.Count - 1] = new IntPathPoint(pathNormal[pathNormal.Count - 1].value, pathReversed[0].length);
            for (int i = 0; i < pathReversed.Count - 1; i++) {
                pathReversed[i] = new IntPathPoint(pathReversed[i].value, pathReversed[i + 1].length);
            }
            Points = pathNormal.Concat(pathReversed).ToList();
        }

        //distance must be less than Length
        public int GetValue(int distance) {
            int pointIndex = 0;
            for (; distance > Points[pointIndex].length; distance -= Points[pointIndex].length, pointIndex++) ;
            if (pointIndex != Points.Count - 1) {
                return Points[pointIndex].value + (Points[pointIndex + 1].value - Points[pointIndex].value) * distance / Points[pointIndex].length;
            } else {
                return Points[pointIndex].value;
            }
        }

        private static IList<IntPathPoint> RemoveLastPointLength(IList<IntPathPoint> points) {
            IList<IntPathPoint> lastLenRemoved = new List<IntPathPoint>(points);
            lastLenRemoved[lastLenRemoved.Count - 1] = new IntPathPoint(lastLenRemoved[lastLenRemoved.Count - 1].value, 0);
            return lastLenRemoved;
        }

        public override string ToString() {
            return string.Join(", ", Points);
        }
    }
}
