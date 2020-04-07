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
        public static void Log(string data)
        {
            Debug.WriteLine($"[RAPID FORCE]: {data}");
        }
    }
}
