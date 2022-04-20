using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Reference : AtomExpressions
    {
        public string Id { get; private set; }

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

        public Reference(CodeLocation location, string Id) : base(location)
        {
            this.Id = Id;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Scope s = table;
            while (s != null)
            {
                if (s.Compiling.ContainsKey(Id))
                {
                    IsOk = true;
                    Type = table.Compiling[Id];
                    return;
                }
                s = s.Parent;
            }
            IsOk = false;

        }

        public override void Evaluate(Scope table, Action<string> error)
        {
            Scope s = table;
            while (s != null)
            {
                if (s.Executing.ContainsKey(Id))
                {
                    Value = table.Executing[Id].Body;
                    Type = table.Executing[Id].Type;
                    return;
                }
                s = s.Parent;
            }
        }
    }
}
