using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.DependencyInjection
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class DefaultImplmentationAttribute : Attribute
    {
        public DefaultImplmentationAttribute(Type contract, Type implmentation)
        {
            if (runTypes.ContainsKey(contract))
            {
                runTypes.Add(contract, implmentation);
                DefaultUnityMappings.RegisterTypeDefault(contract, implmentation);
            }
        }

        private static Dictionary<Type, Type> runTypes = new Dictionary<Type, Type>();
    }
}
