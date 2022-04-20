using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Program : ASTNode
    {
        public Stack<string> LastColors = new Stack<string>();
        public List<CompilingError> errors = new List<CompilingError>();
        public List<Instruction> Instructions { get; private set; }
        public bool isOk;
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

        public Scope table = new Scope();

        public Program(CodeLocation location) : base(location) { Instructions = new List<Instruction>(); }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            foreach (var i in Instructions)
            {
                i.CheckSemantic(table, errors);
                if (i.IsOk == false)
                {
                    isOk = false;
                    return;
                }
            }
            isOk = true;
        }

        public void Run(Action<Expression> drawer, Action<string> ExecutingError, Action<string> ChangeColor)
        {
            
            foreach (var a in Instructions)
            {
                a.Execute(table, drawer, ExecutingError, ChangeColor, LastColors);
            }
        }
    }
}
