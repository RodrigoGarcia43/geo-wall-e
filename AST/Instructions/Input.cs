using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    abstract class Input : Instruction
    { 
        public abstract object Body { get; set; }
        public Input(CodeLocation location) : base(location)
        {
        }

        public string Id { get; set; }
    }
}
