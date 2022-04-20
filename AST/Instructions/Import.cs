using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E.AST.Instructions
{
    class Import : Instruction
    {
        public Import(CodeLocation location) : base(location)
        {
        }

        public override bool IsOk
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            throw new NotImplementedException();
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            throw new NotImplementedException();
        }
    }
}
