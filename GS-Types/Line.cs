using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Line : GS
    {
        public Point P1 { get; private set; }
        public Point P2 { get; private set; }
        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Line;
            }
        }

        public Line(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public Line()
        {
            P1 = new Point();
            P2 = new Point();
        }

    }
}
