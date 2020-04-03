using System;
using System.Collections.Generic;
using System.Dynamic;
using CitizenFX.Core;

namespace RapidForce
{
    public abstract class Plugin : BaseScript
    {
        protected Plugin()
        {
            Type type = GetType();

            dynamic info = new ExpandoObject();
            info.Assembly = type.Assembly.GetName().FullName;
            info.Version = type.Assembly.GetName().Version.ToString();

            object[] attrs = type.GetCustomAttributes(typeof(PluginAttribute), true);
            if (attrs.Length > 0)
            {
                var p = attrs[0] as PluginAttribute;
                info.Name = p.Name;
            }

            TriggerEvent("rf:RegisterPlugin", info, new Action<string>((error) => throw new Exception(error)));
        }
    }
}
