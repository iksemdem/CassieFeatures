using System.Collections.Generic;
using CassieFeatures.Utilities;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Warhead;
using MEC;
using PlayerRoles;
using Log = Exiled.API.Features.Log;

namespace CassieFeatures
{
    internal class EventHandlers
    {
        // This is for warhead event
        public static bool WasWarheadAnnounced;
        public static bool ActualLeverState;
        
        // This is for Camera Scanner CI entering facility
        public static bool WasCiSpottedInside;
        
        // This is for Camera Scanner SCP leaving facility
        public static bool WasScpSpottedOutside;
        
        
        
        internal void OnWaitingForPlayers()
        {
            // This is for Door Locker
            if (Plugin.Instance.Config.IsLockingDoorsEnabled)
            {
                HandleDoorAction.LockDoors();
            }
        }
        internal void OnRoundStart()
        {
            // Creating colliders for gate A and B, both inside and outside
            HandleCreatingColliders.CreateColliders();
            
            // This is for Camera Scanner SCP leaving facility
            Log.Debug("setting that scp was not inside");
            WasScpSpottedOutside = false;
            // This is for Camera Scanner CI entering facility
            Log.Debug("setting that ci was not inside");
            WasCiSpottedInside = false;
            // This is for Warhead lever change
            Log.Debug("setting warhead lever status to true");
            ActualLeverState = true;

            // This is for Door Locker
            if (Plugin.Instance.Config.IsLockingDoorsEnabled)
            {
                HandleDoorAction.ActionOnDoor();
            }
            
            // This is for timed CASSIEs
            HandleCassieAnnouncements.SendTimedCassies();
        }

        internal void OnDead(DiedEventArgs ev)
        {
            // This is for Tesla
            if (Plugin.Instance.Config.IsTeslaFeatureEnabled)
            {
                List<Team> teamsListDeathIsAnnouncedByCassie =
                    Plugin.Instance.Config.CassieAnnouncesDeathOnTeslaOnTeams;

                Log.Debug("Player Died");

                if (ev.DamageHandler.Type is not Exiled.API.Enums.DamageType.Tesla) return;

                Log.Debug("Player died on Tesla");

                RoleTypeId playersOldRole = ev.TargetOldRole;
                Team playersOldTeam = RoleExtensions.GetTeam(playersOldRole);

                if (!teamsListDeathIsAnnouncedByCassie.Contains(playersOldTeam)) return;

                Log.Debug("Player was in the list of death teams");

                var cassieMessage = HandleReplacingPlaceholders.ReplacePlaceholdersTeam(Plugin.Instance.Config.TeslaCassie.Content,
                    playersOldTeam);
                var cassieMessageText =
                    HandleReplacingPlaceholders.ReplacePlaceholdersTeam(Plugin.Instance.Config.TeslaCassie.Subtitles,
                        playersOldTeam);
                
                Timing.CallDelayed(Plugin.Instance.Config.TeslaCassie.Delay, () =>
                {
                    Cassie.MessageTranslated(cassieMessage, cassieMessageText, false,
                        Plugin.Instance.Config.TeslaCassie.IsNoisy,
                        Plugin.Instance.Config.TeslaCassie.ShowSubtitles);
                    Log.Debug(
                        $"Sent cassie: {cassieMessage}, with subtitles: {cassieMessageText}, was it noisy: {Plugin.Instance.Config.TeslaCassie.IsNoisy}, did it had subtitles: {Plugin.Instance.Config.TeslaCassie.ShowSubtitles}");
                }, Server.Host.GameObject);

            }
        }

        internal void TriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            // This is for Tesla
            if (Plugin.Instance.Config.IsTeslaFeatureEnabled)
            {
                List<Team> teamsThatDoesntTriggerTesla = Plugin.Instance.Config.TeslaDoesNotActivateOnTeams;
                if (teamsThatDoesntTriggerTesla.Contains(RoleExtensions.GetTeam(ev.Player.Role)))
                {
                    ev.IsAllowed = false;
                }
            }
        }

        internal void OnSpawn(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam == Faction.FoundationEnemy)
            {
                Log.Debug("chaos insurgency is spawning");
                // if selected for it to happen only one time don't do the next part of the code
                if (Plugin.Instance.Config.ShouldCameraScannerAnnounceCiEnteringOnlyOneTime)
                {
                    Log.Debug("ci spawned, the feature to announce only one time is turned on, so NOT setting that ci was not outside");
                    return;
                }

                WasCiSpottedInside = false;
                Log.Debug("ci spawned, the feature to announce only one time is turned on, so setting that ci was not inside");
            }
        }

        internal void OnChangingWarheadLever(ChangingLeverStatusEventArgs ev)
        {
            Log.Debug("changing warhead lever");
            Log.Debug($"Old lever status is: {ev.CurrentState}");
            Log.Debug($"Current lever status is: {ActualLeverState}");
            
            if (Plugin.Instance.Config.IsWarheadFeatureEnabled)
            {
                if (Plugin.Instance.Config.ShouldWarheadAnnounceOnlyOneTime)
                {
                    if (WasWarheadAnnounced)
                    {
                        return;
                    }
                    WasWarheadAnnounced = true;
                }
                
                if (!HandleCooldowns.IsWarheadOnCooldown)
                {
                    HandleCooldowns.WarheadCooldown();

                    Team playersTeam = ev.Player.Role.Team;
                    HandleCassieAnnouncements.WarheadCassie(ActualLeverState, playersTeam);
                }
                else
                {
                    Log.Debug("Warhead is on cooldown");
                }
            }
            
            ActualLeverState = ev.CurrentState;
        }
    }
}