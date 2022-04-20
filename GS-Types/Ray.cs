using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Ray : GS
    {
        public Point Start { get; private set; }
        public Point Direction { get; private set; }
        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Ray;
            }
        }

        public Ray(Point start, Point direction)
        {
            Start = start;
            Direction = direction;
        }
        public Ray()
        {
            Start = new Point();
            Direction = new Point();
        }
    }
}
