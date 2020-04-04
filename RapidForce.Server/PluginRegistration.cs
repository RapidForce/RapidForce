using System;
using System.Reflection;

namespace RapidForce
{
    internal class PluginRegistration
    {
        public string Title { get; }

        public string Version { get; }

        public PluginRegistration(string assembly, string title)
        {
            var name = new AssemblyName(assembly);
            if (string.IsNullOrEmpty(title))
            {
                title = name.Name;
            }
            Title = title;
            Version = name.Version.ToString();
        }

        public override string ToString()
        {
            return $"{Title} {Version}";
        }
    }
}
