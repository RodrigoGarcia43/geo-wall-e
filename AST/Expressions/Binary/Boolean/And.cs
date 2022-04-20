using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class And : BinaryExpressions
    {
        public And(CodeLocation location) : base(location)
        {
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

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Type = ExpressionType.Number;
            Left.CheckSemantic(table, errors);
            Right.CheckSemantic(table, errors);
            IsOk = (Left.IsOk && Right.IsOk);
        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            Left.Evaluate(table, error);
            Right.Evaluate(table, error);
            if (Toolbox.IsFalse(Left) || Toolbox.IsFalse(Right))
                Value = 0;
            else
                Value = 1;
        }
    }
}
