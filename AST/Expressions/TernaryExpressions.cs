using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class TernaryExpressions : Expression
    {
        public TernaryExpressions(CodeLocation location) : base(location)
        {
        }

        public Expression first { get; set; }
        public Expression second { get; set; }
        public Expression third { get; set; }
        
    }
}
