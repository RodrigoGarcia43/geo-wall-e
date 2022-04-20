using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class ConstanAssigment : Assigment
    {
        
        public Expression Body { get; set; }        

        public override bool IsOk
        {
            get; set;
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            Body.Evaluate(table, error);
            table.Executing.Add(Id, new VarInfo(Body.Value, Body.Type));
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Body.CheckSemantic(table, errors);

            if (!Body.IsOk)
            {
                IsOk = false;
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "No se puede asignar una expresion con bateo"));
                return;
            }
            Scope s = table;
            while (s!= null)
            {
                if (s.Compiling.ContainsKey(Id))
                {
                    IsOk = false;
                    errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Id ya usado"));
                }
                s = s.Parent;
            }
            
            IsOk = true;
            table.Compiling.Add(Id, Body.Type);
        }
        public ConstanAssigment(CodeLocation location) : base (location)
        {

        }
    }
}
