using System;

namespace RapidForce
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CallAttribute : Attribute
    {
        public string Name { get; }

        public CallAttribute(string name)
        {
            Name = name;
        }
    }
}
