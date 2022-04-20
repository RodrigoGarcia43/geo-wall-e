using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Restore : Instruction
    {
        public Restore(CodeLocation location) : base(location)
        {
        }

        public override bool IsOk
        {
            get; set;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            IsOk = true;
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            if (LastColors.Count > 0)
                LastColors.Pop();
            if (LastColors.Count > 0)
            {
                ChangeColor(LastColors.Peek());
                return;
            }
            ChangeColor("Black");
        }
    }
}
