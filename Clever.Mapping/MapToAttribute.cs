using System;
namespace Clever.Mapping
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    sealed class MapToAttribute : Attribute
    {
        private string[] properties;

        public MapToAttribute(params string[] properties)
        {
            this.properties = properties;
        }

        public string[] Properties { get { return properties;  } }
    }
}
