using System;
using System.Collections.Generic;
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

        private CallRegistry.Entry currentCall;

        public Script()
        {
            Plugins = new PluginRegistry(this);
            Calls = new CallRegistry(this);

#if DEBUG
            API.RegisterCommand("start_call", new Action<int, List<object>, string>((int source, List<object> args, string rawCommand) =>
            {
                if (args == null || args.Count == 0)
                {
                    return;
                }
                StartCall(args[0].ToString());
            }), true);

            API.RegisterCommand("end_call", new Action<int, List<object>, string>((int source, List<object> args, string rawCommand) => EndCall()), true);
#endif
        }

        public void StartCall(string name)
        {
            if (currentCall != null)
            {
                return;
            }
            var call = Calls[name];
            if (call == null)
            {
                return;
            }
            TriggerPluginEvent(call.Plugin.Id, Event.Server.Plugin.CallStart, name, new Action<bool>((success) =>
            {
                if (success)
                {
                    currentCall = call;
                }
            }));
        }

        public void EndCall()
        {
            if (currentCall == null)
            {
                return;
            }
            TriggerPluginEvent(currentCall.Plugin.Id, Event.Server.Plugin.CallEnd, new Action<bool>((success) =>
            {
                if (success)
                {
                    currentCall = null;
                }
            }));
        }

        public void TriggerPluginEvent(int pluginId, string name, params object[] args)
        {
            TriggerEvent(Event.Plugin(pluginId) + name, args);
        }

        public void AddHandler(string eventName, Delegate handler)
        {
            EventHandlers[eventName] += handler;
        }

        public static void Log(string data)
        {
            Debug.WriteLine($"[RAPID FORCE]: {data}");
        }
    }
}
