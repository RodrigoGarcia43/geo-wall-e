using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public abstract class ASTNode
    {
        public abstract bool IsOk { get; set; }
        public abstract void CheckSemantic(Scope table, List<CompilingError> errors);   
        public CodeLocation Location { get; set; }     

        public ASTNode(CodeLocation location)
        {
            Location = location;
        }
    }
}