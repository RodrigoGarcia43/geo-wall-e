using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public static class Toolbox
    {
        public static int Count(Sequence s)
        {
            return s.count();
        }

        public static bool IsColor(Token t)
        {
            return (t.Value == TokenValues.Red || t.Value == TokenValues.Gray || t.Value == TokenValues.Green || t.Value == TokenValues.Yellow || t.Value == TokenValues.White || t.Value == TokenValues.Magenta || t.Value == TokenValues.Cyan);
        }

        public static bool CanDrawIt(Expression e)
        {
            return (e.Type == ExpressionType.CircleSequence || e.Type == ExpressionType.LineSequence || e.Type == ExpressionType.RaySequence || e.Type == ExpressionType.SegmentSequence || e.Type == ExpressionType.PointSequence || e.Type == ExpressionType.Arc || e.Type == ExpressionType.Circle || e.Type == ExpressionType.Line || e.Type == ExpressionType.Point || e.Type == ExpressionType.Ray || e.Type == ExpressionType.Segment || e.Type == ExpressionType.Anytype || e.Type == ExpressionType.Sequence);
        }

        public static bool CanDrawSequence(Expression s)
        {
            List<Expression> body = s.Value as List<Expression>;
            if (body.Any())
                return false;
            ExpressionType bodyType = body[1].Type;
            return ((!(s is ThreePoints)) && bodyType == ExpressionType.Arc || bodyType == ExpressionType.Circle || bodyType == ExpressionType.Line || bodyType == ExpressionType.Point || bodyType == ExpressionType.Ray || bodyType == ExpressionType.Segment);
        }

        public static bool IsFalse(Expression exp)
        {
            if (exp.Value.Equals("0") || exp.Value.Equals("Undefined"))
                return true;
            if (exp.Type == ExpressionType.Sequence)
            {
                IEnumerable<object> exp_ = exp.Value as IEnumerable<object>;
                if (exp_.Any<object>())
                    return true;
            }
            return false;
        }

        public static bool IsDefaultFunction(string s)
        {
            return (s == "Circle" || s == "Line" || s == "Ray" || s == "Segment" || s == "Arc" || s=="Measure" || s == "Point");
        }

        public static double PointDistance(Point a, Point b)
        {
            return Math.Sqrt((Math.Pow((a.X - b.X), 2)) + (Math.Pow((a.Y - b.Y), 2)));
        }

        public static void IsIntrinsecFunction(string s, out ExpressionType type)
        {
            type = ExpressionType.Anytype;
            if (s == "Circle")
            {
                type = ExpressionType.Circle;
                
            }
            if (s == "Line")
            {
                type = ExpressionType.Line;
                
            }
            if (s == "Ray")
            {
                type = ExpressionType.Ray;
                
            }
            if (s == "Segment")
            {
                type = ExpressionType.Segment;
                
            }
            if (s == "Arc")
            {
                type = ExpressionType.Arc;
                
            }
            
        }
    }
}
