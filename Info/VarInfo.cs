using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class VarInfo
    {
        public ExpressionType Type { get; set; }
        public object Body { get; set; }

        public VarInfo(object body, ExpressionType type)
        {
            Type = type;
            Body = body;
        }
    }
}
