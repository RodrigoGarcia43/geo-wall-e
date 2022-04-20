using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class AtomExpressions : Expression
    {
        public AtomExpressions(CodeLocation location) : base(location)
        {
        }
    }
}
