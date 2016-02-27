using System;

namespace Clever.Function
{
    public static class Composition
    {
        public static TOut Transform<TIn, TOut>(this TIn value, Func<TIn, TOut> f1)
        {
            return f1(value);
        }

        // Parameterize
        public static Func<TIn, TOut> Parameterize<TIn, TP1, TOut>(this Func<TIn, TP1, TOut> func, TP1 p1)
        {
            return (TIn input) => func(input, p1);
        }
        public static Func<TIn, TOut> Parameterize<TIn, TP1, TP2, TOut>(this Func<TIn, TP1, TP2, TOut> func, TP1 p1, TP2 p2)
        {
            return (TIn input) => func(input, p1, p2);
        }
        public static Func<TIn, TOut> Parameterize<TIn, TP1, TP2, TP3, TOut>(this Func<TIn, TP1, TP2, TP3, TOut> func, TP1 p1, TP2 p2, TP3 p3)
        {
            return (TIn input) => func(input, p1, p2, p3);
        }
        public static Func<TIn, TOut> Parameterize<TIn, TP1, TP2, TP3, TP4, TOut>(this Func<TIn, TP1, TP2, TP3, TP4, TOut> func, TP1 p1, TP2 p2, TP3 p3, TP4 p4)
        {
            return (TIn input) => func(input, p1, p2, p3, p4);
        }

        // Compose
        public static Func<TIn, TOut> Compose<TIn, TMid, TOut>(this Func<TIn, TMid> first, Func<TMid, TOut> second)
        {
            return (TIn input) => input.Transform(first).Transform(second);
        }

        // Category Theory Stuff
        public static IPossibility<TOut> PossiblyTransform<TIn, TOut>(this IPossibility<TIn> possibility, Func<TIn, TOut> f1)
        {
            return possibility.GetPossibility(f1);
        }
    }
}
