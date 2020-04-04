using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using CitizenFX.Core;

namespace RapidForce
{
    public abstract class Plugin : BaseScript
    {
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

            TriggerEvent(Event.Server.RegisterPlugin, info, new Action<string>((error) => throw new Exception(error)));
        }
    }
}
