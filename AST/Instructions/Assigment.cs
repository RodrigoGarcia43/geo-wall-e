using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    abstract class Assigment : Instruction
    {
        public Assigment(CodeLocation location) : base(location)
        {
        }

        public string Id { get; set; }
        //public abstract Expression Body { get; set; }
    }
}
