using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidForce
{
    public interface ICallRequest
    {
        string Name { get; }

        /// <summary>
        /// Accepts the call request.
        /// </summary>
        void Accept();

        /// <summary>
        /// Denies the call request.
        /// </summary>
        void Deny();
    }
}
