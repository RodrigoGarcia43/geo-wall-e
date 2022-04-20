using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Not : UnaryExpressions        
    {
        
        public Not(CodeLocation location) : base(location)
        {
        }

        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Number;
            }
            set { }
        }

        public override object Value
        {
            get; set;
        }

        public override bool IsOk
        {
            get; set;
        }

        

        public override void Evaluate( Scope table, Action<string> error)
        {
            if (Exp.Value.Equals(0) || Exp.Value.Equals("Undefined") || Exp.Value.Equals("{}"))
                Value = 1;
            else
                Value = 0; 
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Exp.CheckSemantic(table, errors);
            if (Exp.IsOk)
                IsOk = true;
            else IsOk = false;
        }

        
    }
}
