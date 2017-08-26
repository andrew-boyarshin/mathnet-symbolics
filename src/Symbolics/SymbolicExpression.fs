﻿namespace MathNet.Symbolics

open System.Numerics
open MathNet.Numerics
open MathNet.Symbolics

[<StructuredFormatDisplay("{Expression}")>]
type SymbolicExpression(expression:Expression) =

    member this.Expression = expression


    // LEAFS - Integer
    static member Zero = SymbolicExpression(Expression.Zero)
    static member One = SymbolicExpression(Expression.One)
    static member Two = SymbolicExpression(Expression.Two)
    static member MinusOne = SymbolicExpression(Expression.MinusOne)
    static member FromInt32(x:int32) = SymbolicExpression(Expression.FromInt32(x))
    static member FromInt64(x:int64) = SymbolicExpression(Expression.FromInt64(x))
    static member FromInteger(x:BigInteger) = SymbolicExpression(Expression.FromInteger(x))
    static member FromIntegerFraction(n:BigInteger, d:BigInteger) = SymbolicExpression(Expression.FromIntegerFraction(n, d))
    static member FromRational(x:BigRational) = SymbolicExpression(Expression.FromRational(x))

    // LEAFS - Approximations
    static member Real(approximation:float) = SymbolicExpression(Expression.Real(approximation))

    // LEAFS - Constants
    static member I = SymbolicExpression(Expression.I)
    static member E = SymbolicExpression(Expression.E)
    static member Pi = SymbolicExpression(Expression.Pi)

    // LEAFS - Mathematical Symbols
    static member PositiveInfinity = SymbolicExpression(Expression.PositiveInfinity)
    static member NegativeInfinity = SymbolicExpression(Expression.NegativeInfinity)
    static member ComplexInfinity = SymbolicExpression(Expression.ComplexInfinity)
    static member Undefined = SymbolicExpression(Expression.Undefined)

    // LEAFS - Symbols
    static member Variable(name:string) = SymbolicExpression(Expression.Symbol(name))


    // PARSING

    static member Parse(infix:string) : SymbolicExpression =
        SymbolicExpression(Infix.parseOrThrow infix)

    static member ParseMathML(mathml:string) : SymbolicExpression =
        SymbolicExpression(MathML.parse mathml)

    static member ParseExpression(expression:System.Linq.Expressions.Expression) : SymbolicExpression =
        SymbolicExpression(Linq.parse expression)

    static member ParseQuotation(quotation:Microsoft.FSharp.Quotations.Expr) : SymbolicExpression =
        SymbolicExpression(Quotations.parse quotation)


    // FORMATTING

    override this.ToString() : string =
        Infix.format expression

    member this.ToInternalString() : string =
        Infix.formatStrict expression

    member this.ToLaTeX() : string =
        LaTeX.format expression

    member this.ToMathML() : string =
        MathML.formatContentStrict expression

    member this.ToSemanticMathML() : string =
        MathML.formatSemanticsAnnotated expression


    // EVALUATION & COMPILATION


    // CASTING

    static member op_Implicit (x:Expression) : SymbolicExpression = SymbolicExpression(x)
    static member op_Implicit (x:string) : SymbolicExpression = SymbolicExpression.Parse(x)

    static member op_Implicit (x:int32) : SymbolicExpression = SymbolicExpression.FromInt32(x)
    static member op_Implicit (x:int64) : SymbolicExpression = SymbolicExpression.FromInt64(x)
    static member op_Implicit (x:BigInteger) : SymbolicExpression = SymbolicExpression.FromInteger(x)
    static member op_Implicit (x:BigRational) : SymbolicExpression = SymbolicExpression.FromRational(x)
    static member op_Implicit (x:float) : SymbolicExpression = SymbolicExpression.Real(x)

    // bad idea, don't do this
    // static member op_Implicit (x:SymbolicExpression) : Expression = x.Expression


    // OPERATORS

    static member ( ~+ ) (x:SymbolicExpression) = SymbolicExpression(+x.Expression)
    static member ( ~- ) (x:SymbolicExpression) = SymbolicExpression(-x.Expression)
    static member ( + ) ((x:SymbolicExpression), (y:SymbolicExpression)) = SymbolicExpression(x.Expression + y.Expression)
    static member ( - ) ((x:SymbolicExpression), (y:SymbolicExpression)) = SymbolicExpression(x.Expression - y.Expression)
    static member ( * ) ((x:SymbolicExpression), (y:SymbolicExpression)) = SymbolicExpression(x.Expression * y.Expression)
    static member ( / ) ((x:SymbolicExpression), (y:SymbolicExpression)) = SymbolicExpression(x.Expression / y.Expression)

    static member Sum([<System.ParamArray>] summands : SymbolicExpression array) = SymbolicExpression(summands |> Seq.map (fun x -> x.Expression) |> Operators.sumSeq)
    static member Sum(summands : SymbolicExpression seq) = SymbolicExpression(summands |> Seq.map (fun x -> x.Expression) |> Operators.sumSeq)
    static member Product([<System.ParamArray>] factors : SymbolicExpression array) = SymbolicExpression(factors |> Seq.map (fun x -> x.Expression) |> Operators.productSeq)
    static member Product(factors : SymbolicExpression seq) = SymbolicExpression(factors |> Seq.map (fun x -> x.Expression) |> Operators.productSeq)

    member this.Negate() = -this
    member this.Add(x:SymbolicExpression) = this + x
    member this.Subtract(x:SymbolicExpression) = this - x
    member this.Multiply(x:SymbolicExpression) = this * x
    member this.Divide(x:SymbolicExpression) = this / x

    member this.Pow(power:SymbolicExpression) = SymbolicExpression(Expression.Pow(expression, power.Expression))
    member this.Invert() = SymbolicExpression(Expression.Invert(expression))

    member this.Abs() = SymbolicExpression(Expression.Abs(expression))

    member this.Root(n:SymbolicExpression) = SymbolicExpression(Expression.Root(n.Expression, expression))
    member this.Sqrt() = SymbolicExpression(Expression.Sqrt(expression))

    member this.Exp() = SymbolicExpression(Expression.Exp(expression))
    member this.Ln() = SymbolicExpression(Expression.Ln(expression))
    member this.Log() = SymbolicExpression(Expression.Log(expression))
    member this.Log(basis:SymbolicExpression) = SymbolicExpression(Expression.Log(basis.Expression, expression))

    member this.Sin() = SymbolicExpression(Expression.Sin(expression))
    member this.Cos() = SymbolicExpression(Expression.Cos(expression))
    member this.Tan() = SymbolicExpression(Expression.Tan(expression))
    member this.Sec() = SymbolicExpression(Expression.Sec(expression))
    member this.Csc() = SymbolicExpression(Expression.Csc(expression))
    member this.Cot() = SymbolicExpression(Expression.Cot(expression))
    member this.Sinh() = SymbolicExpression(Expression.Sinh(expression))
    member this.Cosh() = SymbolicExpression(Expression.Cosh(expression))
    member this.Tanh() = SymbolicExpression(Expression.Tanh(expression))
    member this.ArcSin() = SymbolicExpression(Expression.ArcSin(expression))
    member this.ArcCos() = SymbolicExpression(Expression.ArcCos(expression))
    member this.ArcTan() = SymbolicExpression(Expression.ArcTan(expression))