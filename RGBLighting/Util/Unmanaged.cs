using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBLighting.Util {
    public static class Unmanaged {
        public static T[] PtrToArray<T>(IntPtr ptr, int count, int elementSize) {
            T[] array = new T[count];
            for (int i = 0; i < count; i++) {
                array[i] = Marshal.PtrToStructure<T>(IntPtr.Add(ptr, i * elementSize));
            }
            return array;
        }
    }
}
