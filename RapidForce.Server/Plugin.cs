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
    }
}
