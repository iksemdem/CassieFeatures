using System;
using Exiled.API.Features;

namespace CassieFeatures
{
    internal class Plugin : Plugin<Config>
    {
        private EventHandlers _eventHandlers;

        public override string Author => "iksemdem";
        public override string Name => "CassieFeatures";
        public override string Prefix => "cassie_features";
        public override Version Version => new Version(1, 4, 0);
        public static Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            _eventHandlers = new EventHandlers();
            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            UnregisterEvents();

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += _eventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted += _eventHandlers.OnRoundStart;
            Exiled.Events.Handlers.Player.Died += _eventHandlers.OnDead;
            Exiled.Events.Handlers.Player.TriggeringTesla += _eventHandlers.TriggeringTesla;
            Exiled.Events.Handlers.Server.RespawningTeam += _eventHandlers.OnSpawn;
            Exiled.Events.Handlers.Warhead.ChangingLeverStatus += _eventHandlers.OnChangingWarheadLever;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= _eventHandlers.OnRoundStart;
            Exiled.Events.Handlers.Player.Died -= _eventHandlers.OnDead;
            Exiled.Events.Handlers.Player.TriggeringTesla -= _eventHandlers.TriggeringTesla;
            Exiled.Events.Handlers.Server.RespawningTeam -= _eventHandlers.OnSpawn;
            Exiled.Events.Handlers.Warhead.ChangingLeverStatus -= _eventHandlers.OnChangingWarheadLever;
        }
    }
}