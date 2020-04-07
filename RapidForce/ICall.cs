using System;

namespace RapidForce
{
    public interface ICall
    {
        void Accept();

        void Deny();

        void End();
    }
}
