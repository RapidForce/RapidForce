using System;

namespace RapidForce
{
    public static class Event
    {
        public const string Prefix = "rf:";

        public static class Server
        {
            public const string RegisterPlugin = Prefix + "RegisterPlugin";
        }
    }
}
