namespace RapidForce
{
    public class Pursuit
    {
        /// <summary>
        /// The client initiating the pursuit.
        /// </summary>
        public int ClientId { get; private set; }
        /// <summary>
        /// Any AI (local) backup units that are pursuing.
        /// </summary>
        public int[] LocalBackupId { get; private set; }
        /// <summary>
        /// The AI (local) that is being pursued.
        /// </summary>
        public int LocalId { get; private set; }

        public Pursuit(int clientId, int localId)
        {
            ClientId = clientId;
            LocalId = localId;
        }
    }
}
