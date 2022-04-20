using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Point : GS
    {
        public double X { get; }
        public double Y { get; }

        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Point;
            }
        }

        public Point(double x , double y)
        {
            X = x;
            Y = y;
        }

        public Point()
        {
            X = r.Next(0, 500);
            Y = r.Next(0, 500);
        }
    }
}
