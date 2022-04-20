using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Sequence : AtomExpressions
    {
        public List<Expression> value;
        
                
        public ExpressionType BodyType { get; private set; }
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
            get
            {
                return value;
            }
            set { }
        }

        public override bool IsOk
        {
            get; set;
        }

        public Sequence(CodeLocation location) : base(location) { value = new List<Expression>(); }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            BodyType = value[0].Type;
            foreach (var i in value)
            {
                {
                    i.CheckSemantic(table, errors);
                    if (!i.IsOk || i.Type != BodyType)
                        IsOk = false;
                }
            }
            IsOk = true;
        }

        public int count()
        {
            return value.Count();
        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            foreach (var item in value)
            {
                item.Evaluate(table, error);
            }
        }
    }
}
