using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RapidForce
{
    internal class Script : BaseScript
    {
        public PluginRegistry Plugins { get; }

        public CallRegistry Calls { get; }

        public new EventHandlerDictionary EventHandlers => base.EventHandlers;

        public static readonly Random Random = new Random();

        public Script()
        {
            Plugins = new PluginRegistry(this);
            Calls = new CallRegistry(this);
        }

        public void TriggerPluginEvent(int pluginId, string name, params object[] args)
        {
            TriggerEvent(Event.Plugin(pluginId) + name, args);
        }

        public void AddHandler(string eventName, Delegate handler)
        {
            EventHandlers[eventName] += handler;
        }
    }
}
