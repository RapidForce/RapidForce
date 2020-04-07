using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace RapidForce
{
    public class Script : BaseScript
    {
        public Script()
        {
            EventHandlers[Event.Client.StartPursuit] += new Action<Pursuit>(OnStartPursuit);
            EventHandlers[Event.Client.StopPursuit] += new Action<Pursuit>(OnStopPursuit);
        }

        private void OnStartPursuit(Pursuit pursuit)
        {
            // make pursuit.LocalId flee.
            // create blip for local on clientId.
            // end pursuit when cancelled or too far away.
        }

        private void OnStopPursuit(Pursuit pursuit)
        {
            // clean up pursuit data.
            // dismiss AI entities.
        }

        public static void Log(string data)
        {
            Debug.WriteLine($"[RAPID FORCE]: {data}");
        }
    }
}
