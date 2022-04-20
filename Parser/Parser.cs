using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWall_E
{
    public class Parser
    {

        public Parser(TokenStream stream)
        {
            Stream = stream;
        }
        public TokenStream Stream { get; private set; }

        public bool ParseProgram(out Program program)
        {
            program = new Program(new CodeLocation());

            if (!Stream.CanLookAhead(0))
                return false;
            Instruction instruction;
            while (ParseInstruction(out instruction))
            {
                program.Instructions.Add(instruction);

                if (!Stream.Next() || Stream.LookAhead().Value != TokenValues.StatementSeparator)
                    return false;

                if (!Stream.Next())
                    break;
            }
            return Stream.End;
        }

        #region ParseInstructions

        private bool ParseInstruction(out Instruction instruction)
        {
            return (ParseInput(out instruction) || ParseAssigment(out instruction) || ParseDraw(out instruction) || ParseColor(out instruction) || ParseRestore(out instruction) || ParseImport(out instruction));
        }

        private bool ParseImport(out Instruction instruction)
        {
            instruction = null;
            return false;
        }

        private bool ParseRestore(out Instruction instruction)
        {
            instruction = new Restore(Stream.LookAhead().Location);

            if (Stream.LookAhead().Value != TokenValues.Restore)
                return false;

            return true;
        }

        private bool ParseColor(out Instruction instruction)
        {
            Color color = (instruction = new Color(Stream.LookAhead().Location)) as Color;

            if (Stream.LookAhead().Value != TokenValues.Color || !Stream.CanLookAhead(1) || !Toolbox.IsColor(Stream.LookAhead(1)))
                return false;
            Stream.MoveNext(1);
            color.ToColor = Stream.LookAhead().Value;
            return true;
        }

        private bool ParseDraw(out Instruction instruction)
        {
            Draw item = (instruction = new Draw(Stream.LookAhead().Location)) as Draw;

            if (Stream.LookAhead().Value != TokenValues.Draw || !Stream.Next())
                return false;

            Expression exp;
            if (!ParseExpression(out exp))
            {
                Stream.MoveBack(1);
                return false;
            }
            item.Body = exp;
            return true;
        }

        #region Inputs
        private bool ParseInput(out Instruction instruction)
        {
            return (ParseInputCircle(out instruction)          || ParseInputLine(out instruction)
                 || ParseInputPoint(out instruction)           || ParseInputRay(out instruction) 
                 || ParseInputSegment(out instruction)         || ParseInputSequencePoint(out instruction)
                 || ParseInputSequenceCircle(out instruction)  || ParseInputSequenceLine(out instruction) 
                 || ParseInputSequenceRay(out instruction)     || ParseInputSequenceSegment(out instruction));
        }

        private bool ParseInputSequenceSegment(out Instruction instruction)
        {
            int ini = Stream.Position;
            SequenceInputSegment item = (instruction = new SequenceInputSegment(Stream.LookAhead().Location)) as SequenceInputSegment;
            if (Stream.LookAhead().Value != TokenValues.Segment || !Stream.Next(TokenValues.Sequence) || !Stream.Next(TokenType.Identifier))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Id = Stream.LookAhead().Value;
            return true;
        }

        private bool ParseInputSequenceRay(out Instruction instruction)
        {
            int ini = Stream.Position;
            SequenceInputRay item = (instruction = new SequenceInputRay(Stream.LookAhead().Location)) as SequenceInputRay;
            if (Stream.LookAhead().Value != TokenValues.Ray || !Stream.Next(TokenValues.Sequence) || !Stream.Next(TokenType.Identifier))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Id = Stream.LookAhead().Value;
            return true;
        }

        private bool ParseInputSequencePoint(out Instruction instruction)
        {
            int ini = Stream.Position;
            SequenceInputPoint item = (instruction = new SequenceInputPoint(Stream.LookAhead().Location)) as SequenceInputPoint;
            if (Stream.LookAhead().Value != TokenValues.Point || !Stream.Next(TokenValues.Sequence) || !Stream.Next(TokenType.Identifier))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Id = Stream.LookAhead().Value;
            return true;
        }

        
        private bool ParseInputSequenceLine(out Instruction instruction)
        {
            int ini = Stream.Position;
            SequenceInputLine item = (instruction = new SequenceInputLine(Stream.LookAhead().Location)) as SequenceInputLine;
            if (Stream.LookAhead().Value != TokenValues.Line || !Stream.Next(TokenValues.Sequence) || !Stream.Next(TokenType.Identifier))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Id = Stream.LookAhead().Value;
            return true;
        }

        private bool ParseInputSequenceCircle(out Instruction instruction)
        {
            int ini = Stream.Position;
            SequenceInputCircle item = (instruction = new SequenceInputCircle(Stream.LookAhead().Location)) as SequenceInputCircle;
            if (Stream.LookAhead().Value != TokenValues.Circle || !Stream.Next(TokenValues.Sequence) || !Stream.Next(TokenType.Identifier))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Id = Stream.LookAhead().Value;
            return true;
        }

        

        private bool ParseInputSegment(out Instruction instruction)
        {
            InputSegment item = (instruction = new InputSegment(Stream.LookAhead().Location)) as InputSegment;

            if (Stream.LookAhead().Value != TokenValues.Segment || !Stream.Next(TokenType.Identifier))
                return false;

            item.Id = Stream.LookAhead().Value;

            return true;
        }

        private bool ParseInputRay(out Instruction instruction)
        {
            InputRay item = (instruction = new InputRay(Stream.LookAhead().Location)) as InputRay;

            if (Stream.LookAhead().Value != TokenValues.Ray || !Stream.Next(TokenType.Identifier))
                return false;

            item.Id = Stream.LookAhead().Value;

            return true;
        }

        private bool ParseInputPoint(out Instruction instruction)
        {
            InputPoint item = (instruction = new InputPoint(Stream.LookAhead().Location)) as InputPoint;

            if (Stream.LookAhead().Value != TokenValues.Point || !Stream.Next(TokenType.Identifier))
                return false;

            item.Id = Stream.LookAhead().Value;

            return true;
        }

        

        private bool ParseInputLine(out Instruction instruction)
        {
            InputLine item = (instruction = new InputLine(Stream.LookAhead().Location)) as InputLine;

            if (Stream.LookAhead().Value != TokenValues.Line || !Stream.Next(TokenType.Identifier))
                return false;

            item.Id = Stream.LookAhead().Value;

            return true;
        }

        private bool ParseInputCircle(out Instruction instruction)
        {
            InputCircle item = (instruction = new InputCircle(Stream.LookAhead().Location)) as InputCircle;

            if (Stream.LookAhead().Value != TokenValues.Circle || !Stream.Next(TokenType.Identifier))
                return false;

            item.Id = Stream.LookAhead().Value;

            return true;
        }

        

        #endregion

        private bool ParseAssigment(out Instruction instruction)
        {
            return (ParseConstantAssigment(out instruction) || ParseSequenceAssigment(out instruction) || ParseThreePointsAssigment(out instruction) || ParseFunctionAssigment(out instruction));
        }

        private bool ParseFunctionAssigment(out Instruction instruction)
        {
            int ini = Stream.Position;
            FunctionAssigment item = (instruction = new FunctionAssigment(Stream.LookAhead().Location)) as FunctionAssigment;

            if (Stream.LookAhead().Type != TokenType.Identifier || !Stream.Next(TokenValues.OpenBracket) || !Stream.Next())
            {
                return false;
            }
            item.Id = Stream.LookAhead(-2).Value;

            while (true)
            {
                if (Stream.LookAhead().Value != TokenValues.ValueSeparator)
                {
                    if (Stream.LookAhead().Type != TokenType.Identifier)
                    {
                        Stream.MoveBack(Stream.Position - ini);
                        return false;
                    }
                    else
                        item.Params.Add(Stream.LookAhead().Value);
                    if (Stream.Next(TokenValues.ClosedBracket))
                        break;
                    if(!Stream.Next())
                    {
                        Stream.MoveBack(Stream.Position - ini);
                        return false;
                    }
                }
            }
            Expression body;
            if (!Stream.Next(TokenValues.Assign) || !Stream.Next() || !ParseExpression(out body))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Body = body;
            return true;
        }

        private bool ParseSequenceAssigment(out Instruction instruction)
        {
            int ini = Stream.Position;
            SequenceAssigment item = (instruction = new SequenceAssigment(Stream.LookAhead().Location)) as SequenceAssigment;

            if (Stream.LookAhead().Type != TokenType.Identifier)
                return false;

            while (Stream.Next(TokenValues.Assign))
            {
                if (Stream.LookAhead().Value != TokenValues.ValueSeparator)
                {
                    if (Stream.LookAhead().Type != TokenType.Identifier)
                    {
                        Stream.MoveBack(Stream.Position - ini);
                        return false;
                    }
                    else
                        item.Id.Add(Stream.LookAhead().Value);
                }
            }

            Expression body;
            if (!Stream.Next() || !ParseExpression(out body))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Body = body;

            return true;

        }

        private bool ParseThreePointsAssigment(out Instruction instruction)
        {
            int ini = Stream.Position;
            ThreePointsAssigment item = (instruction = new ThreePointsAssigment(Stream.LookAhead().Location)) as ThreePointsAssigment;

            if (Stream.LookAhead().Type != TokenType.Identifier)
                return false;

            while (Stream.Next(TokenValues.Assign))
            {
                if (Stream.LookAhead().Value != TokenValues.ValueSeparator)
                {
                    if (Stream.LookAhead().Type != TokenType.Identifier)
                    {
                        Stream.MoveBack(Stream.Position - ini);
                        return false;
                    }
                    else
                        item.Id.Add(Stream.LookAhead().Value);
                }
            }

            Expression left;
            Expression rigth;
            if (!Stream.Next(TokenValues.OpenCurlyBraces) || Stream.Next() || !ParseExpression(out left) || !Stream.Next("...") || !Stream.Next() || !ParseExpression(out rigth))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }

            return true;
        }

        private bool ParseConstantAssigment(out Instruction instruction)
        {
            ConstanAssigment assig = (instruction = new ConstanAssigment(Stream.LookAhead().Location)) as ConstanAssigment;

            if (Stream.LookAhead().Type != TokenType.Identifier || !Stream.Next(TokenValues.Assign))
                return false;

            assig.Id = Stream.LookAhead(-1).Value;

            Expression exp;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            if (!ParseExpression(out exp))
            {
                Stream.MoveBack(2);
                return false;
            }

            assig.Body = exp;
            return true;
        }

        #endregion

        #region ParseExpressions
        private bool ParseExpression(out Expression exp)
        {
            return ParseExpressionLv1(null, out exp);
        }

        #region ExpressionLvls
        private bool ParseExpressionLv1(Expression left, out Expression exp)
        {
            exp = null;
            Expression newleft;
            return (ParseConditional(out exp) || ParseLetIn(out exp) || (ParseExpressionLv2(left, out newleft) && ParseExpressionLv1_(newleft, out exp)));
        }

        private bool ParseExpressionLv1_(Expression left, out Expression exp)
        {
            if (!ParseOr(left, out exp) && !ParseAnd(left, out exp))
                exp = left;
            return true;
        }


        private bool ParseExpressionLv2(Expression left, out Expression exp)
        {
            exp = null;
            Expression newLeft;
            return ParseNot(out exp) || (ParseExpressionLv3(left, out newLeft) && ParseExpressionLv2_(newLeft, out exp));
        }

        private bool ParseExpressionLv2_(Expression left, out Expression exp)
        {
            if (!ParseEq(left, out exp) && !ParseNotEq(left, out exp) && !ParseGt(left, out exp) && !ParseGtEq(left, out exp) && !ParseLt(left, out exp) && !ParseLtEq(left, out exp))
                exp = left;
            return true;
        }



        private bool ParseExpressionLv3(Expression left, out Expression exp)
        {
            exp = null;
            Expression newLeft;
            return ParseExpressionLv4(left, out newLeft) && ParseExpressionLv3_(newLeft, out exp);
        }

        private bool ParseExpressionLv3_(Expression left, out Expression exp)
        {
            if (!ParseAdd(left, out exp) && !ParseSub(left, out exp))
                exp = left;
            return true;
        }

        private bool ParseExpressionLv4(Expression left, out Expression exp)
        {
            exp = null;
            Expression newLeft;
            return ParseExpressionLv5(left, out newLeft) && ParseExpressionLv4_(newLeft, out exp);
        }
        private bool ParseExpressionLv4_(Expression left, out Expression exp)
        {
            if (!ParseMult(left, out exp) && !ParseDiv(left, out exp) && !ParseMod(left, out exp))
                exp = left;
            return true;
        }

        private bool ParseExpressionLv5(Expression left, out Expression exp)
        {
            return ParseCallFunction(out exp) || ParseExpressionLv6(left, out exp);
        }

        private bool ParseExpressionLv6(Expression left, out Expression exp)
        {
            return (ParseNumber(out exp) || ParseText(out exp) || ParseUndefined(out exp) || ParseSequence(out exp) || ParseReference(out exp) || ParseOpenBracket(out exp));
        }



        #endregion

        #region ExpressionParsers

        private bool ParseAdd(Expression left, out Expression exp)
        {
            add sum = (exp = new add(Stream.LookAhead().Location)) as add;

            sum.Left = left;

            if (left == null || !Stream.Next(TokenValues.Add))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv4(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            sum.Right = rigth;

            return ParseExpressionLv3_(sum, out exp);
        }

        private bool ParseMult(Expression left, out Expression exp)
        {
            mul mult = (exp = new mul(Stream.LookAhead().Location)) as mul;
            mult.Left = left;

            if (left == null || !Stream.Next(TokenValues.Mul))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv5(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            mult.Right = rigth;
            return ParseExpressionLv4_(mult, out exp);
        }

        private bool ParseDiv(Expression left, out Expression exp)
        {
            div item = (exp = new div(Stream.LookAhead().Location)) as div;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.Div))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv5(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv4_(item, out exp);
        }

        private bool ParseMod(Expression left, out Expression exp)
        {
            mod item = (exp = new mod(Stream.LookAhead().Location)) as mod;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.Mod))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv5(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv4_(item, out exp);
        }

        private bool ParseNotEq(Expression left, out Expression exp)
        {
            NotEqual item = (exp = new NotEqual(Stream.LookAhead().Location)) as NotEqual;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.NotEquals))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv3(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv2_(item, out exp);
        }

        private bool ParseEq(Expression left, out Expression exp)
        {
            Equal item = (exp = new Equal(Stream.LookAhead().Location)) as Equal;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.Equals))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv3(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv2_(item, out exp);
        }

        private bool ParseGt(Expression left, out Expression exp)
        {
            Greater item = (exp = new Greater(Stream.LookAhead().Location)) as Greater;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.Greater))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv3(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv2_(item, out exp);
        }

        private bool ParseGtEq(Expression left, out Expression exp)
        {
            GEqual item = (exp = new GEqual(Stream.LookAhead().Location)) as GEqual;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.GreaterOrEquals))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv3(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv2_(item, out exp);
        }

        private bool ParseLt(Expression left, out Expression exp)
        {
            Less item = (exp = new Less(Stream.LookAhead().Location)) as Less;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.Less))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv3(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv2_(item, out exp);
        }

        private bool ParseLtEq(Expression left, out Expression exp)
        {
            LEqual item = (exp = new LEqual(Stream.LookAhead().Location)) as LEqual;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.LessOrEquals))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv3(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv2_(item, out exp);
        }

        private bool ParseOr(Expression left, out Expression exp)
        {
            Or item = (exp = new Or(Stream.LookAhead().Location)) as Or;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.Or))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv2(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv1_(item, out exp);
        }

        private bool ParseAnd(Expression left, out Expression exp)
        {
            And item = (exp = new And(Stream.LookAhead().Location)) as And;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.And))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv2(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv1_(item, out exp);
        }

        private bool ParseSub(Expression left, out Expression exp)
        {
            sub item = (exp = new sub(Stream.LookAhead().Location)) as sub;
            item.Left = left;

            if (left == null || !Stream.Next(TokenValues.Or))
                return false;

            if (!Stream.Next())
            {
                Stream.MoveBack(1);
                return false;
            }

            Expression rigth;
            if (!ParseExpressionLv4(null, out rigth))
            {
                Stream.MoveBack(2);
                return false;
            }
            item.Right = rigth;
            return ParseExpressionLv3_(item, out exp);
        }



        private bool ParseNot(out Expression exp)
        {
            Not item = (exp = new Not(Stream.LookAhead().Location)) as Not;

            if (Stream.LookAhead().Value != TokenValues.Not || !Stream.Next())
                return false;

            Expression value;

            if (!ParseExpressionLv1(null, out value))
            {
                Stream.MoveBack(1);
                return false;
            }

            item.Exp = value;

            return true;
        }

        private bool ParseConditional(out Expression exp)
        {
            IfThenElse item = (exp = new IfThenElse(Stream.LookAhead().Location)) as IfThenElse;

            int ini = Stream.Position;
            Expression first;
            Expression second;
            Expression third;

            if (  Stream.LookAhead().Value != TokenValues.If   || !Stream.Next() || !ParseExpressionLv1(null, out first)  || !Stream.Next()
               || Stream.LookAhead().Value != TokenValues.Then || !Stream.Next() || !ParseExpressionLv1(null, out second) || !Stream.Next()
               || Stream.LookAhead().Value != TokenValues.Else || !Stream.Next() || !ParseExpressionLv1(null, out third))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.first = first;
            item.second = second;
            item.third = third;

            return true;
        }

        private bool ParseLetIn(out Expression exp)
        {
            int ini = Stream.Position;
            LetIn item = (exp = new LetIn(Stream.LookAhead().Location)) as LetIn;

            if (Stream.LookAhead().Value != TokenValues.Let || !Stream.Next())
            {
                return false;
            }

            Instruction instruction;
            while (ParseInstruction(out instruction))
            {
                item.Instructions.Add(instruction);

                if (!Stream.Next() || Stream.LookAhead().Value != TokenValues.StatementSeparator)
                {
                    Stream.MoveBack(Stream.Position - ini);
                    return false;
                }

                if (Stream.Next(TokenValues.In))
                    break;
                if (!Stream.Next())
                {
                    Stream.MoveBack(Stream.Position - ini);
                    return false;
                }
            }

            Expression In;

            if (Stream.LookAhead().Value != TokenValues.In || !Stream.Next() || !ParseExpressionLv1(null, out In))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }

            item.In = In;
            return true;

        }

        private bool ParseCallFunction(out Expression exp)
        {
            int ini = Stream.Position;
            CallFunction item = (exp = new CallFunction(Stream.LookAhead().Location)) as CallFunction;

            if (Stream.LookAhead().Type != TokenType.Identifier && (!Toolbox.IsDefaultFunction(Stream.LookAhead().Value)))
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            item.Id = Stream.LookAhead().Value;

            if (!Stream.Next(TokenValues.OpenBracket) || !Stream.Next())
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }

            Expression argument;
            while (true)
            {
                if (Stream.LookAhead().Value != TokenValues.ValueSeparator)
                {
                    if (!ParseExpressionLv1(null, out argument))
                    {
                        Stream.MoveBack(Stream.Position - ini);
                        return false;
                    }
                    else
                        item.Values.Add(argument);
                }
                if (Stream.Next(TokenValues.ClosedBracket))
                        break;
                if (!Stream.Next())
                {
                    Stream.MoveBack(Stream.Position - ini);
                    return false;
                }
            }
            return true;
        }

        private bool ParseNumber(out Expression exp)
        {
            exp = null;
            if (Stream.LookAhead(0).Type != TokenType.Number)
                return false;
            exp = new Number(double.Parse(Stream.LookAhead().Value), Stream.LookAhead().Location);
            return true;
        }

        private bool ParseText(out Expression exp)
        {
            exp = null;
            if (Stream.LookAhead(0).Type != TokenType.Text)
                return false;
            exp = new Text(Stream.LookAhead().Value, Stream.LookAhead().Location);
            return true;
        }

        private bool ParseUndefined(out Expression exp)
        {
            exp = new Undefined(Stream.LookAhead().Location);
            return (Stream.LookAhead().Value == "Undefined");


        }

        private bool ParseSequence(out Expression exp)
        {
            int ini = Stream.Position;
            Sequence item = (exp = new Sequence(Stream.LookAhead().Location)) as Sequence;

            if (Stream.LookAhead().Value != TokenValues.OpenCurlyBraces || !Stream.Next())
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }

            Expression argument;
            while (true)
            {
                if (Stream.LookAhead().Value != TokenValues.ValueSeparator)
                {
                    if (!ParseExpressionLv1(null, out argument) )
                    {
                        Stream.MoveBack(Stream.Position - ini);
                        return false;
                    }
                    else
                        item.value.Add(argument);
                }
                if (Stream.Next(TokenValues.ClosedCurlyBraces))
                    break;
                if (!Stream.Next())
                {
                    Stream.MoveBack(Stream.Position - ini);
                    return false;
                }
            }
            return true;
        }

        private bool ParseReference(out Expression exp)
        {
            exp = null;
            if (Stream.LookAhead().Type != TokenType.Identifier)
                return false;
            Reference item = (exp = new Reference(Stream.LookAhead().Location, Stream.LookAhead().Value)) as Reference;
            return true;
        }

        private bool ParseOpenBracket(out Expression exp)
        {
            int ini = Stream.Position;
            exp = null;
            if (Stream.LookAhead().Value != TokenValues.OpenBracket || !Stream.Next())
                return false;

            if (!ParseExpressionLv1(null, out exp))
            {
                Stream.MoveBack(1);
                return false;
            }

            if (Stream.LookAhead().Value != TokenValues.ClosedBracket)
            {
                Stream.MoveBack(Stream.Position - ini);
                return false;
            }
            return true;
        }
        #endregion

        #endregion
    }
}
