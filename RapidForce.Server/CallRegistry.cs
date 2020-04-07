using System;
using System.Collections.Generic;

using CitizenFX.Core;

namespace RapidForce
{
    internal class CallRegistry
    {
        public IEnumerable<Entry> Items => calls.Values;

        private readonly IDictionary<string, Entry> calls = new Dictionary<string, Entry>();

        private readonly Script script;

        public Entry this[string name] => calls[name];

        public CallRegistry(Script script)
        {
            this.script = script;

            script.AddHandler(Event.Server.RegisterCall, new Action<int, string, CallbackDelegate>(Register));
        }

        private void Register(int pluginId, string name, CallbackDelegate callback)
        {
            PluginRegistration plugin = script.Plugins[pluginId];
            if (plugin == null)
            {
                callback?.Invoke("Plugin id does not exist");
                return;
            }
            if (calls.ContainsKey(name))
            {
                callback?.Invoke("Call name already registered");
                return;
            }
            calls[name] = new Entry(plugin, name);
            callback?.Invoke(string.Empty);
        }

        public class Entry
        {
            public PluginRegistration Plugin { get; }

            public string Name { get; }

            public Entry(PluginRegistration plugin, string name)
            {
                Plugin = plugin;
                Name = name;
            }
        }
    }
}
