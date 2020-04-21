using System.Dynamic;

namespace RapidForce
{
    public class Pursuit : IModel
    {
        /// <summary>
        /// The client initiating the pursuit.
        /// </summary>
        public int ClientHandle { get; private set; }

        /// <summary>
        /// The AI (local) that is being pursued.
        /// </summary>
        public int TargetHandle { get; private set; }

        /// <summary>
        /// Any AI (local) backup units that are pursuing.
        /// </summary>
        public int[] AdditionalHandles { get; private set; }

        public int Handle => ClientHandle;

        public Pursuit(int clientHandle, int targetHandle)
        {
            ClientHandle = clientHandle;
            TargetHandle = targetHandle;
        }

        public void Unpack(dynamic model)
        {
            ClientHandle = model.ClientHandle;
            TargetHandle = model.TargetHandle;
            AdditionalHandles = model.AdditionalHandles;
        }

        public ExpandoObject Pack()
        {
            dynamic obj = new ExpandoObject();
            obj.ClientHandle = ClientHandle;
            obj.TargetHandle = TargetHandle;
            obj.AdditionalHandles = AdditionalHandles;

            return obj;
        }
    }
}
