using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Measure : GS
    {
        public double Distance { get; set; }
        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Measure;
            }
        }

        public Measure( double distance )
        {
            Distance = distance;
        }

        public Measure()
        {
            Distance = r.Next(0, 100);
        }
    }
}
