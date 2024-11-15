using System.Collections.Generic;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using Log = Exiled.API.Features.Log;

namespace CassieFeatures
{
    internal class EventHandlers
    {
        internal void OnRoundStart()
        {
            // Creating colliders for gate A and B, both inside and outside
            Utils.CreateColliders();
            
            // This is for Camera Scanner SCP leaving facility
            Log.Debug("setting that scp was not inside");
            Utils.WasScpSpottedOutside = false;
            // This is for Camera Scanner CI entering facility
            Log.Debug("setting that ci was not inside");
            Utils.WasCiSpottedInside = false;
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

                var cassieMessage = Utils.ReplacePlaceholders(Plugin.Instance.Config.DeathOnTeslaCassieAnnouncement,
                    playersOldTeam);
                var cassieMessageText =
                    Utils.ReplacePlaceholders(Plugin.Instance.Config.DeathOnTeslaCassieAnnouncementSubtitles,
                        playersOldTeam);

                Cassie.MessageTranslated(cassieMessage, cassieMessageText, false,
                    Plugin.Instance.Config.ShouldTeslaCassieAnnouncementsBeNoisy,
                    Plugin.Instance.Config.ShouldTeslaCassieAnnouncementsHaveSubtitles);
                Log.Debug(
                    $"Sent cassie: {cassieMessage}, with subtitles: {cassieMessageText}, was it noisy: {Plugin.Instance.Config.ShouldTeslaCassieAnnouncementsBeNoisy}, did it had subtitles: {Plugin.Instance.Config.ShouldTeslaCassieAnnouncementsHaveSubtitles}");
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
            if (ev.NextKnownTeam == Respawning.SpawnableTeamType.ChaosInsurgency)
            {
                Log.Debug("chaos insurgency is spawning");
                // if selected for it to happen only one time don't do the next part of the code
                if (Plugin.Instance.Config.ShouldCameraScannerAnnounceCiEnteringOnlyOneTime)
                {
                    Log.Debug("ci spawned, the feature to announce only one time is turned on, so NOT setting that ci was not outside");
                    return;
                }

                Utils.WasCiSpottedInside = false;
                Log.Debug("ci spawned, the feature to announce only one time is turned on, so setting that ci was not inside");
            }
        }
    }
}