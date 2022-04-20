using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class InputCircle : Input
    {
        private bool isOk;
        private GS body;
        

        public override object Body
        {
            get
            {
                return body;
            }

            set
            {
                
            }
        }

        public override bool IsOk
        {
            get
            {
                return isOk;
            }

            set
            {

            }
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            if (!table.Compiling.ContainsKey(Id))
            {
                table.Compiling.Add(Id, ExpressionType.Circle);
                isOk = true;
            }
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            body = new Circle();
            table.Executing.Add(Id, new VarInfo(body, ExpressionType.Circle));
        }

        public InputCircle(CodeLocation location) : base(location)
        {
        }
    }
}
