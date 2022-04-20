using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class UnaryExpressions : Expression
    {
        public UnaryExpressions(CodeLocation location) : base(location)
        {
        }

        public Expression Exp { get; set; }
        
    }
}
