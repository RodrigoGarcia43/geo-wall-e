using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Color : Instruction
    {
        public string ToColor { get; set; }
        public Color(CodeLocation location) : base(location)
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
            LastColors.Push(ToColor);
            ChangeColor(ToColor);
        }
    }
}
