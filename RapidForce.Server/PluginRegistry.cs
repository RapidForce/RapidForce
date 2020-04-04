using System;
using System.Collections.Generic;

using CitizenFX.Core;

namespace RapidForce
{
    internal class PluginRegistry
    {
        public IEnumerable<PluginRegistration> Plugins => plugins.Values;

        private readonly IDictionary<string, PluginRegistration> plugins = new Dictionary<string, PluginRegistration>();

        public PluginRegistry(Script script)
        {
            script.EventHandlers.Add(Event.Server.RegisterPlugin, new Action<dynamic, CallbackDelegate>(Register));
        }

        private void Register(dynamic info, CallbackDelegate errors)
        {
            if (string.IsNullOrWhiteSpace(info.Assembly))
            {
                errors.Invoke("type invalid");
                return;
            }

            if (plugins.ContainsKey(info.Assembly))
            {
                errors.Invoke("already registered");
                return;
            }

            plugins[info.Assembly] = new PluginRegistration(info.Assembly, info.Title);
        }
    }
}
