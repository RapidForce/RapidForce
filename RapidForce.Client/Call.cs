using System;

namespace RapidForce
{
    public abstract class Call : ICall
    {
        public virtual void End() { }

        public virtual void Start() {  }
    }
}
