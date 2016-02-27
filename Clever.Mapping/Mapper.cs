using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Mapping
{
    public static class Mapping
    {
        public static Mapper<TFrom, TTo> CreateExactMapper<TFrom, TTo>()
        {
            // TODO: Map Exact Reflection
            throw new NotImplementedException();
        }

        public static Mapper<TFrom, TTo> CreateCrossStandardMapper<TFrom, TTo>()
        {
            // TODO: Map based on words
            throw new NotImplementedException();
        }

        public static string[] GetWords(string identifier)
        {
            // TODO: Split identifier into words
            throw new NotImplementedException();
        }

        public class Mapper<TFrom, TTo>
        {
            public void Map(TFrom from, TTo to)
            {
                
            }
        }

        public static void InititializeMappingExact(Type fromType, Type toType)
        {
            // TODO: Initialize
        }

        public static void InititializeMappingExact<TFrom, TTo>()
        {
            InititializeMappingExact(typeof(TFrom), typeof(TTo));
        }

        public static void InititializeMappingInterpret(Type fromType, Type toType)
        {
            // TODO: Initialize
        }

        public static void InititializeMappingInterpret<TFrom, TTo>()
        {
            InititializeMappingInterpret(typeof(TFrom), typeof(TTo));
        }

        public static Mapper<TFrom, TTo> Getmapper<TFrom, TTo>()
        {
            return new Mapper<TFrom, TTo>();
        }
    }
}
