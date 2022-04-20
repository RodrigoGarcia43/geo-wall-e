using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Draw : Instruction
    {
        
        public Expression Body { get; set; }
                
        public Draw(CodeLocation location) : base(location)
        {
        }

        public override bool IsOk
        {
            get; set;
        }

        public override void CheckSemantic(Scope table, List<CompilingError> errors)
        {
            Body.CheckSemantic(table, errors);
            if (!Toolbox.CanDrawIt(Body))
            {
                IsOk = false;
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Eso no se puede dibujar"));
                return;                
            }
            IsOk = true;
        }

        internal override void Execute(Scope table, Action<Expression> drawer, Action<string> error, Action<string> ChangeColor, Stack<string> LastColors)
        {
            Body.Evaluate(table, error);
            if ((Body.Type == ExpressionType.Sequence && !Toolbox.CanDrawSequence(Body)) || !Toolbox.CanDrawIt(Body))
                error("mala mia, no te puedo dibujar eso, pero no lo supe hasta tiempo de ejecucion");
            drawer(Body);
        }
    }
}
