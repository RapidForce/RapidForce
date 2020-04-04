using System;
using CitizenFX.Core;

namespace RapidForce
{
    internal class Script : BaseScript
    {
        private readonly PluginRegistry registry;

        public new EventHandlerDictionary EventHandlers => base.EventHandlers;

        public Script()
        {
            registry = new PluginRegistry(this);
        }

        public void AddHandler(string eventName, Delegate handler)
        {
            EventHandlers[eventName] += handler;
        }
    }
}
