using System;
using Exiled.API.Enums;
using Exiled.API.Features;

using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;
using MapEvent = Exiled.Events.Handlers.Map;

namespace ChaosHacker
{
    public class ChaosHacker : Plugin<Config>
    {
        private static readonly Lazy<ChaosHacker> LazyInstance = new Lazy<ChaosHacker>(() => new ChaosHacker());
        public static ChaosHacker Instance => LazyInstance.Value;

        public override PluginPriority Priority => PluginPriority.Medium;

        public override string Author => "Michal78900";
        public override Version Version => new Version(1, 0, 0);

        private ChaosHacker() { }

        private Handler handler;

        public override void OnEnabled()
        {
            base.OnEnabled();

            handler = new Handler();

            ServerEvent.RespawningTeam += handler.OnTeamRespawn;

            PlayerEvent.Left += handler.OnPlayerLeft;
            PlayerEvent.Died += handler.OnPlayerDied;
            PlayerEvent.ChangingRole += handler.OnPlayerRoleChange;




        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            ServerEvent.RespawningTeam -= handler.OnTeamRespawn;

            PlayerEvent.Left -= handler.OnPlayerLeft;
            PlayerEvent.Died -= handler.OnPlayerDied;
            PlayerEvent.ChangingRole -= handler.OnPlayerRoleChange;

            handler = null;
        }
    }
}
