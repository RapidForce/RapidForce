using System;
using CitizenFX.Core;

namespace RapidForce.Demo
{
    [Call("Demo.PursuitInProgress")]
    internal class PursuitInProgress : Call
    {
        public override void Start()
        {
            Debug.WriteLine("Pursuit call start");
        }

        public override void End()
        {
            Debug.WriteLine("Pursuit call end");
        }
    }
}
