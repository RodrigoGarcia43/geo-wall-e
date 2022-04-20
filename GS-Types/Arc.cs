using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Arc : GS
    {
        public Circle Circ { get; private set; }
        public Ray R1 { get; private set; }
        public Ray R2 { get; private set; }
        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Arc;
            }            
        }

        public Arc(Circle circ, Point p1, Point p2)
        {
            Circ = circ;
            R1 = new Ray(Circ.Center, p1);
            R2 = new Ray(Circ.Center, p2);
        }

        public Arc()
        {
            Circ = new Circle();
            R1 = new Ray();
            R2 = new Ray();
        }
    }
}
