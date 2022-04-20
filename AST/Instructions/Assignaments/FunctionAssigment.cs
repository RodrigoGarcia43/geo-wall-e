using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class FunctionAssigment : Instruction
    {
        public FunctionAssigment(CodeLocation location) : base(location)
        {
            Params = new List<string>();
        }

        public string Id { get; set; }
        public List<string> Params { get; set; }
        public Expression Body { get; set; }
        public override bool IsOk
        {
            get; set;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            if (table.Functions.ContainsKey(Id) || table.IntrinsecFunctions.ContainsKey(Id))
            {
                IsOk = false;
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "El nombre de esa uncion ya esta en uso"));
                return;
            }

            IsOk = true;
            table.Functions.Add(Id, new FunctionInfo(Params, Body));
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            if (!table.Functions.ContainsKey(Id))
                table.Functions.Add(Id, new FunctionInfo(Params, Body));
        }
    }
}
