using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class ThreePoints : BinaryExpressions
    {
        public ThreePoints(CodeLocation location) : base(location)
        {
        }

        public bool IsInfinite { get { return Right == null; }  }

        public override ExpressionType Type
        {
            get
            {
                return ExpressionType.Sequence;
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

        public override void Evaluate(Scope table, Action<string> error)
        {
            if (Right == null)
                Value = new EnumerableInteger((int)Left.Value);
            else
                Value = new EnumerableInteger((int)Left.Value, (int)Right.Value);
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            //if (!(Left is Number left) || !(Right is Number right))
            Left.CheckSemantic(table, errors);
            Right.CheckSemantic(table, errors);
            Number left, right;
            if (!(Left is Number) || !(Right != null && Right is Number))
            {
                IsOk = false;
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "secuencia mal declarada"));
                return;
            }
            left = Left as Number;
            if (Right != null)
                right = Right as Number;
            else
                right = null;
            if (!(left.IsInt) || ((right != null) && !(right.IsInt)))
            {
                IsOk = false;
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Secuencia mal declarada"));
                return;
            }
            IsOk = true;                
        }

        
    }
}
