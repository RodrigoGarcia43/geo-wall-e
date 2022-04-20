using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class LetIn : AtomExpressions
    {
        public LetIn(CodeLocation location) : base(location)
        {
            Instructions = new List<Instruction>();
        }

        public override bool IsOk
        {
            get; set;
        }

        public override ExpressionType Type
        {
            get; set;
        }

        public override object Value
        {
            get; set;

        }
        public List<Instruction> Instructions { get; set; }
        public Expression In { get; set; }



        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Scope newTeble = table.CreateChild();
            
            foreach (var a in Instructions)
            {
                a.CheckSemantic(newTeble, errors);
                if (!a.IsOk)
                {
                    errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Hay una instruccion dando palo al Let In"));
                    IsOk = false;
                    return;
                }
            }
            In.CheckSemantic(newTeble, errors);
            if (!In.IsOk)
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Hay una expresion dando palo al Let In"));
                IsOk = false;
                return;
            }
            IsOk = true;
            Type = In.Type;
        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            Scope newTeble = table.CreateChild();
            
            foreach (var a in Instructions)
            {
                a.Execute(newTeble, item => empty(""), error, item => empty(item), new Stack<string>());
            }
            In.Evaluate(newTeble, error);
            Value = In.Value;
            Type = In.Type;
        }

        private void empty(string item)
        {
            
        }
    }
}
