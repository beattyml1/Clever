using Clever.Collection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Clever.Mapping
{
    public static class Mapping
    {
        public static void Map<TFrom, TTo>(TFrom fromVal, TTo toVal)
        {
            Action<object, object> map;
            try
            {
                map = mappingFunctions[typeof(TFrom)][typeof(TTo)];
            }
            catch
            {
                RegisteExactMap<TFrom, TTo>();
                map = mappingFunctions[typeof(TFrom)][typeof(TTo)];
            }
            map(fromVal, toVal);
        }

        public static void Map(Type tfrom, Type tto, object fromVal, object toVal)
        {
            Action<object, object> map;
            try
            {
                map = mappingFunctions[tfrom][tto];
            }
            catch
            {
                RegisteExactMap(tfrom, tto);
                map = mappingFunctions[tfrom][tto];
            }
            map(fromVal, toVal);
        }

        public static void RegisteExactMap<TFrom, TTo>()
        {
            RegisterMap<TFrom, TTo>(PropertiesAreExactMatch);
        }

        public static void RegisterWordMap<TFrom, TTo>()
        {
            RegisterMap<TFrom, TTo>(PropertiesAreWordMatch);
        }

        public static void RegisteExactMap(Type tfrom, Type tto)
        {
            RegisterMap(tfrom, tto, PropertiesAreExactMatch);
        }

        public static void RegisterWordMap(Type tfrom, Type tto)
        {
            RegisterMap(tfrom, tto, PropertiesAreWordMatch);
        }

        public static void RegisterMap<TFrom, TTo>(Func<string, string, bool> matchFunction)
        {
            RegisterMap(typeof(TFrom), typeof(TTo), matchFunction);
        }

        public static bool PropertiesAreExactMatch(string first, string second)
        {
            return first == second;
        }

        public static bool PropertiesAreWordMatch(string first, string second)
        {
            return GetLowerWords(first) == GetLowerWords(second);
        }

        public static string[] GetAdditionalMappingFromAttributes(PropertyInfo property)
        {
            return property.CustomAttributes.OfType<MapToAttribute>().SelectMany(x => x.Properties).ToArray();
        }

        public static string[] GetLowerWords(string identifier)
        {
            return Parser.GetWords(identifier).Select(x => x.ToLower()).ToArray();
        }

        public static void RegisterMap(Type tfrom, Type tto, Func<string, string, bool> matchFunction)
        {
            var propertyLookups = GetPropertyMatches(tfrom, tto, matchFunction);
            mappingFunctions[tfrom][tto] = (fromObj, toObj) => DoMap(fromObj, toObj, propertyLookups);
        }

        private static void DoMap(object fromObj, object toObj, Dictionary<PropertyInfo, PropertyInfo> propertyLookups)
        {
            propertyLookups.ForEach(mapping => mapping.Value.SetValue(toObj, mapping.Key.GetValue(fromObj)));
        }

        public static Dictionary<PropertyInfo, PropertyInfo> GetPropertyMatches(Type t1, Type t2, Func<string, string, bool> matchFunction)
        {
            return (from p1 in t1.GetProperties()
                    from p2 in t1.GetProperties()
                    where matchFunction(p1.Name, p2.Name) || AttributeMapMatch(p1, p2, matchFunction)
                    select new KeyValuePair<PropertyInfo, PropertyInfo>(p1, p2))
                    .ToDictionary();
        }

        public static bool AttributeMapMatch(PropertyInfo prop1, string prop2Name, Func<string, string, bool> matchFunction)
        {
            return GetAdditionalMappingFromAttributes(prop1).Any(x => matchFunction(x, prop2Name));
        }

        public static bool AttributeMapMatch(PropertyInfo prop1, PropertyInfo prop2, Func<string, string, bool> matchFunction)
        {
            return AttributeMapMatch(prop1, prop2.Name, matchFunction) || AttributeMapMatch(prop2, prop1.Name, matchFunction);
        }

        private static readonly Dictionary<Type, Dictionary<Type, Action<object, object>>> mappingFunctions = new Dictionary<Type, Dictionary<Type, Action<object, object>>>();

        private static void SetPropertyRecursive(Type type, string propertyName, object model, object value, Func<string, string, bool> matchFunction)
        {
            var objectProps = type.GetProperties().Where(x => x.PropertyType.IsValueType == false && x.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)) == false);
            var arrayProps = type.GetProperties().Where(x => x.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)));
            var valueProps = type.GetProperties().Where(x => x.PropertyType.IsValueType);
            var matchingProps = valueProps.Where(x => matchFunction(x.PropertyType.Name, propertyName));
            
            var objectValueGetters = objectProps.Select(prop => new KeyValuePair<PropertyInfo, Func<object, object>>(prop, (_ => new[] { prop.GetValue(_) })));
            var objectsInArrayGetters = arrayProps.Select(prop => new KeyValuePair<PropertyInfo, Func<object, object>>(prop, (_ => ((IEnumerable)prop.GetValue(model)).OfType<object>().ToArray())));
            var objectsToAlterGetters = objectsInArrayGetters.Concat(objectValueGetters).GroupBy(x => x.Key.PropertyType, x => x.Value);

            objectsToAlterGetters.ForEach(tgroup => tgroup.ForEach(o => SetPropertyRecursive(tgroup.Key, propertyName, o, value, matchFunction)));
            matchingProps.ForEach(x => x.SetValue(model, value));
        }
    }
}