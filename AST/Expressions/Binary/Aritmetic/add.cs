using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class add : BinaryExpressions
    {
        public add(CodeLocation location) : base(location)
        {
        }

        public override ExpressionType Type
        {
            get; set;
        }


        public override object Value
        {
            get; set;
        }

        public override bool IsOk
        {
            get; set;
        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            Right.Evaluate(table, error);
            Left.Evaluate(table, error);
            Tuple<object, ExpressionType> item = (table.Operations[new Tuple<ExpressionType, ExpressionType, string>(Right.Type, Left.Type, "+")](Right, Left)) as Tuple<object, ExpressionType>;
            Value = (double)item.Item1;
            Type = item.Item2;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Right.CheckSemantic(table, errors);
            Left.CheckSemantic(table, errors);
            if (Right.Type == ExpressionType.Anytype || Left.Type == ExpressionType.Anytype)
            {
                Type = ExpressionType.Anytype;
                IsOk = true;
                return;
            }
            if (table.CheckOperations.ContainsKey(new Tuple<ExpressionType, ExpressionType, string>(Right.Type, Left.Type, "+")))
            {
                Type = table.CheckOperations[new Tuple<ExpressionType, ExpressionType, string>(Right.Type, Left.Type, "+")];
                IsOk = true;
                return;
            }
            errors.Add(new CompilingError(Location, ErrorCode.Invalid, "No puedes sumar eso"));
        }
    }
}
