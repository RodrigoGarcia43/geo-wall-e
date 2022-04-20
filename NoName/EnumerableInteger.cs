using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class EnumerableInteger : IEnumerable<int>
    {
        int a;
        int? b;
        public EnumerableInteger(int a, int? b = null)
        {
            this.a = a;
            this.b = b;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new EnumeratorInteger(a, b);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
