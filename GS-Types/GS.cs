using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class GS
    {
        static public Random r = new Random();
        public abstract ExpressionType Type { get; }
    }
}
