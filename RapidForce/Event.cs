using System;

namespace RapidForce
{
    public static class Event
    {
        public const string Prefix = "rf:";

        public static string Plugin(int id) => $"{Prefix}Plugin:{id}:";

        public static class Server
        {
            public const string RegisterPlugin = Prefix + "RegisterPlugin";

            public const string RegisterCall = Prefix + "RegisterCall";

            public static class Plugin
            {
                public const string Load = "Load";

                public const string CallStart = "CallStart";

                public const string CallEnd = "CallEnd";
            }
        }
    }
}
