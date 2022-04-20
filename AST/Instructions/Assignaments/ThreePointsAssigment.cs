using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class ThreePointsAssigment : Assigment
    {
        private bool isOk = false;

        public ThreePointsAssigment(CodeLocation location) : base(location)
        {
        }

        public ThreePoints Body
        {
            get; set;
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public List<string> Id { get; private set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
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
            foreach (var a in Id)
            {
                if (table.Compiling.ContainsKey(a))
                    throw new NotImplementedException("Agregar error");
            }
            Body.CheckSemantic(table, errors);
            if (!Body.IsOk )
                throw new NotImplementedException("Agregar error");
            isOk = true;
            for (int i = 0; i < Id.Count - 1; i++)
            {
                if (Id[i] == "_")
                    continue;
                if (Body.Right != null && i > (int)Body.Right.Value - (int)Body.Left.Value)
                {
                    for (int j = i; j < Id.Count - 1; j++)
                    {
                        table.Compiling.Add(Id[j], ExpressionType.Undefined);
                    }
                    break;
                }
                table.Compiling.Add(Id[i], ExpressionType.Number);
            }
            if (Id[Id.Count - 1] != "_")
            {
                table.Compiling.Add(Id[Id.Count - 1], ExpressionType.Sequence);
            }

        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            Body.Evaluate(table, error);
            int count = (int)Body.Value;
            for (int i = 0; i < Id.Count - 1; i++)
            {
                count++;
                if (Id[i] == "_")
                    continue;
                if (Body.Right != null && i > (int)Body.Right.Value- (int)Body.Left.Value)
                {
                    for (int j = i; j < Id.Count - 1; j++)
                    {
                        if (Id[i] == "_")
                            continue;
                        table.Executing.Add(Id[j], new VarInfo("Undefined", ExpressionType.Undefined));
                    }
                    break;
                }
                table.Executing.Add(Id[i], new VarInfo(count , ExpressionType.Number));
            }

            if (Id[Id.Count - 1] != "_")
            {
                if (Body.Right == null)
                    table.Executing.Add(Id[Id.Count - 1], new VarInfo(new EnumerableInteger(count + (int)Body.Left.Value, null), ExpressionType.InfiniteSequence));
                else
                {
                    List<int> l = new List<int>();
                    for (int i = count + (int)Body.Left.Value; i < (int)Body.Right.Value ; i++)
                    {
                        l.Add(i);
                    }
                    table.Executing.Add(Id[Id.Count - 1], new VarInfo(l, ExpressionType.Sequence));
                }
            }
        }
    }
}
