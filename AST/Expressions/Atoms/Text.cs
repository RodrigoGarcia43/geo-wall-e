using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Text : AtomExpressions
    {
        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Text;
            }
            set
            {

            }
        }

        public override object Value
        {
            get; set;
        }


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

        public Text(string value, CodeLocation location) : base (location) { Value = value; }

        public override void Evaluate(Scope table, Action<string> error)
        {
            
        }

        public override void CheckSemantic(Scope Table, List<CompilingError> errors)
        {
            
        }
    }
}
