using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class SequenceAssigment : Assigment
    {

        public SequenceAssigment(CodeLocation location) : base(location)
        {
        }

        public Expression Body { get; set; }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword


        public List<string> Id { get; set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public override bool IsOk
        {
            get; set;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Body.CheckSemantic(table, errors);
            if (Body.Type != ExpressionType.Sequence)
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Asignacion no valida"));
                IsOk = false;
                return;
            }
            List<Expression> a = (List<Expression>)Body.Value;
            foreach (var id in Id)
            {
                if (table.Compiling.ContainsKey(id))
                {
                    IsOk = false;
                    errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Id en uso"));

                }
            }
            IsOk = true;
            for (int i = 0; i < Id.Count -1; i++)
            {
                if (Id[i] == "_")
                    continue;                     
                if (a.Count < i + 1)
                {
                    for (int j = i; j < Id.Count - 1; j++)
                    {                        
                        table.Compiling.Add(Id[j], ExpressionType.Undefined);
                    }
                    break;
                }                
                table.Compiling.Add(Id[i], a[i].Type);
            }
            if (Id[Id.Count - 1] == "_")
                return;
            table.Compiling.Add(Id[Id.Count - 1], ExpressionType.Sequence);

        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            Body.Evaluate(table, error);
            List<Expression> a = (List<Expression>)Body.Value;            
            for (int i = 0; i < Id.Count - 1; i++)
            {                
                if (Id[i] == "_")
                    continue;
                if (a.Count < i + 1)
                {
                    for (int j = i; j < Id.Count - 1; j++)
                    {
                        table.Executing.Add(Id[j], new VarInfo("Undefined", ExpressionType.Undefined));
                    }
                    break;
                }
                a[i].Evaluate(table, error);
                table.Executing.Add(Id[i], new VarInfo(a[i].Value,a[i].Type));
            }
            if (Id[Id.Count - 1] == "_")
                return;
            List<Expression> rest = new List<Expression>();
            for (int i = Id.Count-1; i < a.Count; i++)
            {
                rest.Add(a[i]);
            }            
            table.Executing.Add(Id[Id.Count - 1], new VarInfo(rest, ExpressionType.Sequence));
        }

        
    }
}
