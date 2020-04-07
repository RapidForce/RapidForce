using System;
using System.Collections.Generic;
using System.Reflection;
using CitizenFX.Core;

namespace RapidForce
{
    internal class PluginRegistry
    {
        private readonly IDictionary<int, PluginRegistration> plugins = new Dictionary<int, PluginRegistration>();

        private readonly Script script;

        public PluginRegistry(Script script)
        {
            this.script = script;

            script.EventHandlers.Add(Event.Server.RegisterPlugin, new Action<dynamic, CallbackDelegate>(Register));
        }

        public PluginRegistration this[int id] => plugins[id];

        private void Register(dynamic info, CallbackDelegate callback)
        {
            if (string.IsNullOrWhiteSpace(info.Assembly))
            {
                callback.Invoke(0, "type invalid");
                return;
            }

            // NOTE(randomsean): Possible race condition here. Should probably lock plugins until we find a valid id. 
            int id;
            do
            {
                id = Script.Random.Next();
            } while (plugins.ContainsKey(id));
            plugins[id] = new PluginRegistration(id, info.Assembly, info.Title);

            callback.Invoke(id, null);
        }
    }

    internal class PluginRegistration
    {
        public int Id { get; }

        public string Title { get; }

        public string Version { get; }

        public PluginRegistration(int id, string assembly, string title)
        {
            var name = new AssemblyName(assembly);
            if (string.IsNullOrEmpty(title))
            {
                title = name.Name;
            }
            Id = id;
            Title = title;
            Version = name.Version.ToString();
        }

        public override string ToString()
        {
            return $"{Title} {Version}";
        }
    }
}
