using System;

namespace RapidForce
{
    public interface ICall
    {
        /// <summary>
        /// Start is called when the call has been accepted.
        /// </summary>
        void Start();

        /// <summary>
        /// End is called when the call has ended.
        /// This method should clean up anything the call spawned or created.
        /// </summary>
        void End();
    }
}
