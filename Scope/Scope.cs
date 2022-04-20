using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Scope
    {
        public Dictionary<string, ExpressionType> Compiling { get; set; }

        public Dictionary<string, VarInfo> Executing { get; set; }

        public Dictionary<string, FunctionInfo> Functions { get; set; }

        public Dictionary<string, Func<List<Expression>, Tuple<object, ExpressionType>>> IntrinsecFunctions { get; set; }

        public Dictionary<Tuple<ExpressionType, ExpressionType, string>, Func<Expression,Expression,object>> Operations { get; set; }

        public Dictionary<Tuple<ExpressionType, ExpressionType, string>, ExpressionType> CheckOperations { get; set; }


        public Scope Parent = null;

        public Scope()
        {
            Executing = new Dictionary<string, VarInfo>();
            Compiling = new Dictionary<string, ExpressionType>();
            Functions = new Dictionary<string, FunctionInfo>();
            IntrinsecFunctions = new Dictionary<string, Func<List<Expression>, Tuple<object, ExpressionType>>> ();
            Operations = new Dictionary<Tuple<ExpressionType,ExpressionType,string>, Func<Expression,Expression, object>>();
            CheckOperations = new Dictionary<Tuple<ExpressionType, ExpressionType, string>, ExpressionType>();
            IncludeCheckOperations(CheckOperations);
            IncludeOperations(Operations);
            IncludeIntrinsecFunctions(IntrinsecFunctions);
        }

        public Scope CreateChild()
        {
            Scope child = new Scope()
            {
                Parent = this                
            };
            //if (this.Parent != null)
            //    child.Parent = this.Parent;
               
            return child;
        }

        #region Operations
        static void IncludeOperations (Dictionary<Tuple<ExpressionType, ExpressionType, string>, Func<Expression, Expression,object>> dic)
        {
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number,"+"), NumAdd);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Sequence, ExpressionType.Sequence, "+"), SequenceAdd);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "-"), NumberSub);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "*"), NumberMul);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Measure, "*"), NumberMessureMul);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Measure, ExpressionType.Number, "*"), NumberMessureMul);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "/"), NumberDiv);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Measure, ExpressionType.Measure, "/"), MeasureDiv);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "%"), NumberMod);
        }

        static Func<Expression, Expression, object> NumAdd = (a, b) => { return new Tuple<object,ExpressionType>( (double)a.Value + (double)b.Value, ExpressionType.Number); };
        static Func<Expression, Expression, object> SequenceAdd = (a, b) => { return new Tuple<object, ExpressionType>((double)a.Value + (double)b.Value,ExpressionType.Sequence); };
        static Func<Expression, Expression, object> NumberSub = (a, b) => { return new Tuple<object, ExpressionType>((double)a.Value - (double)b.Value, ExpressionType.Number); };
        static Func<Expression, Expression, object> NumberMul = (a, b) => { return new Tuple<object, ExpressionType>((double)a.Value * (double)b.Value, ExpressionType.Number); };
        static Func<Expression, Expression, object> NumberMessureMul = (a, b) => { return new Tuple<object, ExpressionType>(Math.Abs((int)a.Value) + (double)b.Value, ExpressionType.Measure); };
        //static Func<Expression, Expression, object> MulMeasure = (a, b) => { return new Tuple<object, ExpressionType>(Math.Abs((int)b.Value) + (double)a.Value; };
        static Func<Expression, Expression, object> NumberDiv = (a, b) => { return new Tuple<object, ExpressionType>((double)a.Value / (double)b.Value, ExpressionType.Number); };
        static Func<Expression, Expression, object> MeasureDiv = (a, b) => { return new Tuple<object, ExpressionType>((int)(double)a.Value / (double)b.Value, ExpressionType.Number); };
        static Func<Expression, Expression, object> NumberMod = (a, b) => { return new Tuple<object, ExpressionType>((double)a.Value % (double)b.Value, ExpressionType.Number); };

        #endregion

        #region CheckOperations
        static void IncludeCheckOperations(Dictionary<Tuple<ExpressionType, ExpressionType, string>, ExpressionType> dic)
        {
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "+"), ExpressionType.Number);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Sequence, ExpressionType.Sequence, "+"), ExpressionType.Sequence);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Sequence, ExpressionType.InfiniteSequence, "+"), ExpressionType.Undefined);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.InfiniteSequence, ExpressionType.Sequence, "+"), ExpressionType.Undefined);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "-"), ExpressionType.Number);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "*"), ExpressionType.Number);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Measure, "*"), ExpressionType.Measure);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Measure, ExpressionType.Number, "*"), ExpressionType.Measure);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "/"), ExpressionType.Number);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Measure, ExpressionType.Measure, "/"), ExpressionType.Number);
            dic.Add(new Tuple<ExpressionType, ExpressionType, string>(ExpressionType.Number, ExpressionType.Number, "%"), ExpressionType.Number);
        }
        #endregion

        #region InstricsecFunctions
        private void IncludeIntrinsecFunctions(Dictionary<string, Func<List<Expression>, Tuple<object, ExpressionType>>> dic)
        {
            dic.Add("Circle", Circle);
            dic.Add("Point", Point);
            dic.Add("measure", measure);
            dic.Add("Line", Line);
            dic.Add("Ray", Ray);
            dic.Add("Segment", Segment);
            dic.Add("Arc", Arc);
        }

        static Func<List<Expression>, Tuple<object, ExpressionType>> Circle 
            = (a) => { if (a.Count != 2 || !(a[0].Value is Point) || !(a[1].Value is double)) { return null; } return new Tuple<object, ExpressionType>( new Circle(a[0].Value as Point, (double)a[1].Value), ExpressionType.Circle); };
        static Func<List<Expression>, Tuple<object, ExpressionType>> Point
            = (a) => { if (a.Count != 2 || !(a[0].Value is double) || !(a[1].Value is double)) { return null; } return new Tuple<object, ExpressionType>(new Point((double)a[0].Value, (double) a[1].Value), ExpressionType.Point); };
        static Func<List<Expression>, Tuple<object, ExpressionType>> measure
            = (a) => { if (a.Count != 2 || !(a[0].Value is Point) || !(a[1].Value is Point)) { return null; } return new Tuple<object, ExpressionType> (Toolbox.PointDistance(a[0].Value as Point, a[1].Value as Point), ExpressionType.Measure); };
        static Func<List<Expression>, Tuple<object, ExpressionType>> Line
            = (a) => { if (a.Count != 2 || !(a[0].Value is Point) || !(a[1].Value is Point)) { return null; } return new Tuple<object, ExpressionType> ( new Line(a[0].Value as Point, a[1].Value as Point), ExpressionType.Line); };
        static Func<List<Expression>, Tuple<object, ExpressionType>> Ray
            = (a) => { if (a.Count != 2 || !(a[0].Value is Point) || !(a[1].Value is Point)) { return null; } return new Tuple<object, ExpressionType> ( new Ray(a[0].Value as Point, a[1].Value as Point), ExpressionType.Ray); };
        static Func<List<Expression>, Tuple<object, ExpressionType>> Segment
            = (a) => { if (a.Count != 2 || !(a[0].Value is Point) || !(a[1].Value is Point)) { return null; } return new Tuple<object, ExpressionType> (new Segment(a[0].Value as Point, a[1].Value as Point), ExpressionType.Segment); };
        static Func<List<Expression>, Tuple<object, ExpressionType>> Arc
            = (a) => { if (a.Count != 3 || !(a[0].Value is Circle) || !(a[0].Value is Point) || !(a[1].Value is Point)) { return null; } return new Tuple<object, ExpressionType> (new Arc(a[0].Value as Circle, a[1].Value as Point, a[2].Value as Point), ExpressionType.Arc); };
        
        #endregion

    }
}
