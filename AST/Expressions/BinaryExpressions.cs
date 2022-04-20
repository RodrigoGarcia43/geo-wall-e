using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class BinaryExpressions : Expression
    {
        public BinaryExpressions(CodeLocation location) : base(location)
        {
        }

        public Expression Right { get; set; }
        public Expression Left { get; set; }
        
    }
}
