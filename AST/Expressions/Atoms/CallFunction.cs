using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class CallFunction : AtomExpressions
    {
        public CallFunction(CodeLocation location) : base(location)
        {
            Values = new List<Expression>();
        }

        public string Id { get; set; }
        public List<Expression> Values { get; set; }
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


        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            ExpressionType type = ExpressionType.Anytype;
            if(table.IntrinsecFunctions.ContainsKey(Id))
            {
                Toolbox.IsIntrinsecFunction(Id, out type);
                IsOk = true;
                Type = type;
                return;
            }
            Scope s = table;
            while (s != null)
            {
                if (s.Functions.ContainsKey(Id))
                {
                    IsOk = true;
                    Type = ExpressionType.Anytype;
                    return;
                }                
                s = s.Parent;
            }
            Type = ExpressionType.Anytype;
            errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Esa funcion no esta declarada"));
        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            foreach (var a in Values)
                a.Evaluate(table, error);

            Scope s = table;
            while (s != null)
            {
                if (s.Functions.ContainsKey(Id))
                {
                    FunctionInfo function = s.Functions[Id];
                    List<string> vars = function.Vars;
                    Expression body = function.Body;
                    Scope newTable = table.CreateChild();
                    for (int i = 0; i < Values.Count; i++)
                    {
                        newTable.Executing.Add(vars[i], new VarInfo(Values[i], Values[i].Type));
                        newTable.Compiling.Add(vars[i], Values[i].Type);
                    }
                    body.CheckSemantic(newTable, new List<CompilingError>());
                    if (body.IsOk)
                    {
                        body.Evaluate(newTable, error);
                        Value = body.Value;
                        Type = body.Type;
                        return;
                    }
                    else
                        error("Hay una funcion dando bateo");
                }

                else if (table.IntrinsecFunctions.ContainsKey(Id))
                {
                    foreach (var item in Values)
                    {
                        item.Evaluate(table, error);
                    }
                    var a = table.IntrinsecFunctions[Id](Values);
                    if (a == null)
                    {
                        error("Hay una funcion dando bateo");
                    }
                    Value = a.Item1;
                    Type = a.Item2;
                    return;
                }
                s = s.Parent;
            }
        }
    }
}
