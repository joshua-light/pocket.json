using System;

namespace Pocket.Json
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class JsonAttribute : Attribute
    {
        public JsonAttribute() { }
        public JsonAttribute(string name) =>
            Name = name;
        
        public string Name { get; }
    }
}