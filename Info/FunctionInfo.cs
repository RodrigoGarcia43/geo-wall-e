using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class FunctionInfo
    {
        public List<string> Vars { get; set; }
        public Expression Body { get; set; }

        public FunctionInfo(List<string> vars, Expression body)
        {
            Vars = vars;
            Body = body;
        }
    }
}
