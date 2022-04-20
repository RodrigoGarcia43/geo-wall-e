using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class Instruction : ASTNode
    {
        public Instruction(CodeLocation location) : base(location)
        {
        }

        internal abstract void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors);
    }
}