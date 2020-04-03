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
    }
}
