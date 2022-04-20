using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Number : AtomExpressions
    {
        public bool IsInt
        {
            get
            {
                int a;
                return int.TryParse(Value.ToString(), out a);
            }
        }

        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Number;
            }
            set { }
        }

        public override object Value { get; set; }

        public override bool IsOk
        {
            get
            {
                return true;
            }

            set
            {
                
            }
        }
        
        public Number(double value, CodeLocation location) : base (location)
        {
            Value = value;
        }
        
        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            
        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            
        }
    }
}
