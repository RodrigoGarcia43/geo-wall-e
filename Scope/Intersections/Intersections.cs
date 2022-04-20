using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    class Intersections
    {
        public virtual GS Item1 { get; set; }
        public virtual ExpressionType Type1 { get; set; }
        public virtual GS Item2 { get; set; }
        public virtual ExpressionType Type2 { get; set; }

        public List<Intersections> inter = new List<Intersections>();

        public Tuple<IEnumerable<Point>,ExpressionType> Intersect(List<Expression> items)
        {
            if (items.Count < 2)
                return null;

            //inter.Add(new CircleIntersect());
            //inter.Add(new LineCircleIntersect());
            //inter.Add(new LineIntersect());
            //inter.Add(new PointCircleIntersect());
            //inter.Add(new PointIntersect());
            //inter.Add(new PointLineIntersect());
            //inter.Add(new PointSegmentIntersect());
            //inter.Add(new SegmentCircleIntersect());
            //inter.Add(new SegmentIntersect());
            //inter.Add(new SegmentLineIntersect());


            var item1 = items[0];
            var item2 = items[1];


            return null;
        }
    }
}
