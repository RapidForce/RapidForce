using System;
using CitizenFX.Core;

namespace RapidForce.Demo
{
    public class Demo : Plugin
    {
        public Demo()
        {
            RegisterCall(typeof(PursuitInProgress));
        }
    }
}
