using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class IfThenElse : TernaryExpressions
    {
        
        public IfThenElse(CodeLocation location) : base(location)
        {
        }

        public override ExpressionType Type { get; set; }

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
            if (first.Equals(0) || first.Equals("{}") || first.Equals("Undefined"))
                Value = third;
            else
                Value = second;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            first.CheckSemantic(table,errors);
            second.CheckSemantic(table,errors);
            third.CheckSemantic(table, errors);
            if(second.Type == ExpressionType.Anytype || third.Type == ExpressionType.Anytype)
            {
                IsOk = true;
                Type = ExpressionType.Anytype;
                return;
            }
            if (first.IsOk && second.IsOk && third.IsOk && second.Type == third.Type)
            {
                IsOk = true;
                Type = second.Type;                
            }
        }        
    }
}
