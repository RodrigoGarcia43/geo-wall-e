using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class Expression : ASTNode
    {
        public Expression(CodeLocation location) : base (location) { }

        public abstract void Evaluate(Scope table, Action<string> error);

        public abstract ExpressionType Type { get; set; }

        public abstract object Value { get; set; }
    }
}
