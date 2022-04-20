using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public enum ExpressionType
    {
        Anytype,
        Text,
        Number,        
        Sequence,
        InfiniteSequence,
        Measure,
        Undefined,              
        Circle,
        Line,
        Ray,
        Segment, 
        Point,
        Arc,
        PointSequence,
        CircleSequence,
        RaySequence,
        LineSequence,
        SegmentSequence,
    }
}
