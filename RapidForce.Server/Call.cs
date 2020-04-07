using System;

namespace RapidForce
{
    public abstract class Call : ICall
    {
        public abstract void Start();

        public abstract void End();
    }
}
