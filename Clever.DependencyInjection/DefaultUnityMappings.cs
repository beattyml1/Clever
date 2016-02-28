using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace Clever.DependencyInjection
{
    class DefaultUnityMappings
    {
        private static readonly List<Action<UnityContainer>> registrations = new List<Action<UnityContainer>>();

        public static void RegisterTypeDefault<TBase, TImp>() where TImp: TBase 
        {
            registrations.Add(container => container.RegisterType<TBase, TImp>());
        }

        public static void RegisterTypeDefault(Type tbase, Type timp)
        {
            registrations.Add(container => container.RegisterType(tbase, timp));
        }

        public static void RegisterDefaults(UnityContainer container)
        {
            registrations.ForEach(r => r(container));
        }
    }
}
