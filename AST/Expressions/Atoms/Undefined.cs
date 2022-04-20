using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Undefined : AtomExpressions
    {
        public Undefined(CodeLocation location) : base(location)
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
            get
            {
                return true;
            }

            set
            {
                
            }
        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            
        }
    }
}
