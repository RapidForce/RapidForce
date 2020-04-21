using System;

using CitizenFX.Core;

namespace RapidForce
{
    public abstract class Plugin : BaseScript
    {
        private IModelCollection<Pursuit> Pursuits { get; } = new RemoteModelCollection<Pursuit>("Pursuits");
    }
}
