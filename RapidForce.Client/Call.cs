using System;

namespace RapidForce
{
    public abstract class Call : ICall
    {
        protected Call()
        {

        }

        public virtual void Accept() { }

        public virtual void Deny() { }

        public virtual void End() { }
    }
}
