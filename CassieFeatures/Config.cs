using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace CassieFeatures
{
    public sealed class Config : IConfig
    {
        [Description("General:")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Settings for Tesla:")]
        public bool IsTeslaFeatureEnabled { get; set; } = true;

        public List<Team> TeslaDoesNotActivateOnTeams { get; set; } = new List<Team>()
        {
            Team.Scientists,
            Team.FoundationForces
        };

        public List<Team> CassieAnnouncesDeathOnTeslaOnTeams { get; set; } = new List<Team>()
        {
            Team.ClassD,
            Team.ChaosInsurgency,
            Team.OtherAlive
        };

        public string DeathOnTeslaCassieAnnouncement { get; set; } =
            "{PlayersTeam} died on tesla . {TeamMembersAlive} {PlayersTeam}s left";
        public string DeathOnTeslaCassieAnnouncementSubtitles { get; set; } =
            "{PlayersTeam} died on tesla. {TeamMembersAlive} {PlayersTeam}s left.";
        public bool ShouldTeslaCassieAnnouncementsBeNoisy { get; set; } = true;
        public bool ShouldTeslaCassieAnnouncementsHaveSubtitles { get; set; } = true;
        [Description("Settings for Camera Scanner (When SCP enters Surface):")]
        public bool IsCameraScannerLookForScpLeavingFeatureEnabled { get; set; } = true;
        public bool ShouldCameraScannerAnnounceScpLeavingOnlyOneTime { get; set; } = true;
        public int CassieDelaySinceScpLeaving { get; set; } = 10;
        public bool ShouldCassieCheckIfScpIsStillOnSurfaceAfterTheDelay { get; set; } = true;
        public string ScpLeavingFacilityCassieAnnouncement { get; set; } =
            "the camera system has detected {ScpRole} outside the facility at {Gate}";
        public string ScpLeavingFacilityCassieAnnouncementSubtitles { get; set; } =
            "The Camera System has detected {ScpRole} outside the Facility at {Gate}.";
        public bool ShouldScpLeavingCassieAnnouncementsBeNoisy { get; set; } = true;
        public bool ShouldScpLeavingCassieAnnouncementsHaveSubtitles { get; set; } = true;
        [Description("Settings for Camera Scanner (When CI enters the Facility):")]
        public bool IsCameraScannerLookingForCiEnteringFeatureEnabled { get; set; } = true;
        [Description("If set to false, cassie wont announce CI untill next CI spawn (option below)")]
        public bool ShouldCameraScannerAnnounceCiEnteringOnlyOneTime { get; set; } = false;
        public int CassieDelaySinceCiEntering { get; set; } = 10;
        public string CiEnteringFacilityCassieAnnouncement { get; set; } =
            "the camera system has detected chaos insurgency agents inside the facility at {Gate}";
        public string CiEnteringFacilityCassieAnnouncementSubtitles { get; set; } =
            "The Camera System has detected Chaos Insurgency Agents inside the Facility at {Gate}.";
        public bool ShouldCiEnteringFacilityCassieAnnouncementsBeNoisy { get; set; } = true;
        public bool ShouldCiEnteringFacilityCassieAnnouncementsHaveSubtitles { get; set; } = true;
    }
}