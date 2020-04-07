using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using CitizenFX.Core;

namespace RapidForce
{
    public abstract class Plugin : BaseScript
    {
        public static List<Pursuit> ActivePursuits { get; private set; }

        protected Plugin()
        {
            Type type = GetType();

            dynamic info = new ExpandoObject();
            info.Assembly = type.Assembly.GetName().ToString();

            var attr = type.Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute)).SingleOrDefault() as AssemblyTitleAttribute;
            if (!string.IsNullOrWhiteSpace(attr?.Title))
            {
                info.Title = attr?.Title;
            }

            TriggerEvent(Event.Server.RegisterPlugin, info, new Action<string>((error) => throw new Exception(error)));
        }

        public static Pursuit Pursuit(int clientId, int localId) => new Pursuit(clientId, localId);
        public static void StartPursuit(Pursuit pursuit)
        {
            ActivePursuits.Add(pursuit);
            Script.Log($"Adding active pursuit {pursuit.LocalId}");
        }
        public static void UpdatePursuit(Pursuit pursuit)
        {

        }
        public static void StopPursuit(Pursuit pursuit)
        {
            if (ActivePursuits.Remove(pursuit))
            {
                Script.Log($"Successfully removed pursuit {pursuit.LocalId}");
                return;
            }
            Script.Log($"Failed to remove pursuit {pursuit.LocalId}");
        }
    }
}
