using System;
using System.Collections.Generic;

using CitizenFX.Core;

namespace RapidForce
{
    internal class PluginRegistry
    {
        public IEnumerable<PluginAttribute> Plugins => plugins.Values;

        private readonly IDictionary<string, PluginAttribute> plugins = new Dictionary<string, PluginAttribute>();

        public PluginRegistry(Script script)
        {
            script.EventHandlers.Add("rf:RegisterPlugin", new Action<dynamic, CallbackDelegate>(Register));
        }

        private void Register(dynamic info, CallbackDelegate errors)
        {
            if (string.IsNullOrWhiteSpace(info.Assembly))
            {
                errors.Invoke("type invalid");
                return;
            }

            if (plugins.Contains(info.Assembly))
            {
                errors.Invoke("already registered");
                return;
            }

            plugins[info.Assembly] = new PluginAttribute()
            {
                Name = info.Name,
            };

            Debug.WriteLine($"Registered plugin {info.Assembly}");
        }
    }
}
