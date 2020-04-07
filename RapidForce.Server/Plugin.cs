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
        protected event Action<ICall> CallReceived;

        private int id;

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

            AddPluginHandler(Event.Server.Plugin.CallReceived, new Action<string>(CreateCall));
        }

        public void AddPluginHandler(string name, Delegate d)
        {
            EventHandlers[Event.Plugin(id) + name] += d;
        }

        private void CreateCall(string name)
        {
            Type t;
            if (!calls.TryGetValue(name, out t))
            {
                return;
            }
            var call = (ICall)Activator.CreateInstance(t);
            CallReceived?.Invoke(call);
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
