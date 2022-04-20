using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class SequenceInputCircle : Input
    {
        public SequenceInputCircle(CodeLocation location) : base(location)
        {
        }

        private bool isOk;
        private object body;


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
                table.Compiling.Add(Id, ExpressionType.Sequence);
                isOk = true;
            }
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            List<Circle> item = new List<Circle>();
            for (int i = 0; i < 5; i++)
            {
                body = new Circle();
                item.Add((Circle)body);
            }

            table.Executing.Add(Id, new VarInfo(item, ExpressionType.CircleSequence));
        }
    }
}

