using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class InputPoint : Input
    {
        public InputPoint(CodeLocation location) : base(location)
        {
        }

        


        public override object Body
        {
            get; set;
        }

        public override bool IsOk
        {
            get; set;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            if (!table.Compiling.ContainsKey(Id))
            {
                table.Compiling.Add(Id, ExpressionType.Point);
                IsOk = true;
            }
            else
                IsOk = false;
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            Body = new Point();
            table.Executing.Add(Id, new VarInfo(Body, ExpressionType.Point));
        }
    }
}
