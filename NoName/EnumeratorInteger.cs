using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoWall_E
{
    internal class EnumeratorInteger : IEnumerator<int>
    {
        private int a;
        private int? b;
        private int value;

        public EnumeratorInteger(int a, int? b = null)
        {
            this.a = a;
            this.b = b;
            value = a;
        }

        public int Current
        {
            get
            {
                return value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return value;
            }
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (b == null || value <= b)
            {
                value++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            value = a;
        }
    }
}