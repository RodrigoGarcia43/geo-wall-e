using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Circle : GS
    {
        public Point Center { get; private set; }
        public double Radio { get; private set; }
        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Circle;
            }
        }

        public Circle( Point center, double radio)
        {
            Center = center;
            Radio = radio;
        }
        public Circle()
        {
            Center = new Point();
            Radio = (int)r.Next(0,500);
        }
    }
}
