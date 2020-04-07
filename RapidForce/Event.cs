namespace RapidForce
{
    public static class Event
    {
        public const string PrefixClient = "rf:Client:";
        public const string PrefixServer = "rf:Server:";

        public static class Client
        {
            public const string Log = PrefixClient + "Log";

            public const string StartPursuit = PrefixClient + "StartPursuit";
            public const string UpdatePursuit = PrefixClient + "UpdatePursuit";
            public const string StopPursuit = PrefixClient + "StopPursuit";
        }

        public static class Server
        {
            public const string Log = PrefixServer + "Log";

            public const string RegisterPlugin = PrefixServer + "RegisterPlugin";
        }
    }
}
