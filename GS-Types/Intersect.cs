using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Intersect : GS
    {
        public List<Point> Points { get; private set; }
        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Sequence;
            }
        }

        public Intersect(List<Point> points)
        {
            Points = points;
        }
    }
}
