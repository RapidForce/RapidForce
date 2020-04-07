using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RapidForce
{
    public abstract class Plugin : BaseScript
    {
        public List<Pursuit> ActivePursuits { get; private set; }

        private int id;
        private Call call;

        private readonly IDictionary<string, Type> calls = new Dictionary<string, Type>();

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

            TriggerEvent(Event.Server.RegisterPlugin, info, new Action<int, string>(Register));
        }

        private void Register(int id, string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            this.id = id;

            AddPluginHandler(Event.Server.Plugin.CallStart, new Action<string, CallbackDelegate>(CreateAndStartCall));
            AddPluginHandler(Event.Server.Plugin.CallEnd, new Action<CallbackDelegate>(EndCall));
        }

        private void CreateAndStartCall(string name, CallbackDelegate callback)
        {
            if (!calls.TryGetValue(name, out Type t))
            {
                callback.Invoke(false);
                return;
            }
            try
            {
                call = (Call)Activator.CreateInstance(t);
                call.Start();
            }
            catch (Exception ex)
            {
                callback.Invoke(false);
                throw new Exception("Could not create and start call", ex);
            }
            callback.Invoke(true);
        }

        private void EndCall(CallbackDelegate callback)
        {
            if (call == null)
            {
                callback.Invoke(false);
                return;
            }
            call.End();
            callback.Invoke(true);
        }

        public void AddPluginHandler(string name, Delegate d)
        {
            EventHandlers[Event.Plugin(id) + name] += d;
        }

        protected void RegisterCall(Type type)
        {
            if (!type.IsSubclassOf(typeof(Call)))
            {
                throw new Exception("Type does not implement Call");
            }

            var attr = type.GetCustomAttribute<CallAttribute>();
            if (attr == null)
            {
                throw new Exception("Type missing Call attribute");
            }
            if (string.IsNullOrWhiteSpace(attr.Name))
            {
                throw new Exception("Call name cannot be null or empty");
            }

            TriggerEvent(Event.Server.RegisterCall, id, attr.Name, new Action<string>((error) =>
            {
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
                calls[attr.Name] = type;
            }));
        }

        /// <summary>
        /// Create a new pursuit instance.
        /// </summary>
        /// <param name="clientId">The pursuing client's ID.</param>
        /// <param name="localId">AI (local) ID being pursued.</param>
        /// <returns></returns>
        public Pursuit Pursuit(int clientId, int localId) => new Pursuit(clientId, localId);
        public void StartPursuit(Pursuit pursuit)
        {
            ActivePursuits.Add(pursuit);
            Script.Log($"Adding active pursuit {pursuit.LocalId}");
            Players[pursuit.ClientId].TriggerEvent(Event.Client.StartPursuit, pursuit);
        }
        public void UpdatePursuit(Pursuit pursuit)
        {

        }
        public void StopPursuit(Pursuit pursuit)
        {
            if (ActivePursuits.Remove(pursuit))
            {
                Script.Log($"Successfully removed pursuit {pursuit.LocalId}");
                return;
            }
            Script.Log($"Failed to remove pursuit {pursuit.LocalId}");
            Players[pursuit.ClientId].TriggerEvent(Event.Client.StopPursuit, pursuit);
        }
    }
}
