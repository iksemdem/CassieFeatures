using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
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

        public CassieAnnouncement TeslaCassie { get; set; } = new CassieAnnouncement(
            "{PlayersTeam} died on tesla . {TeamMembersAlive} {PlayersTeam}s left",
            "{PlayersTeam} died on tesla. {TeamMembersAlive} {PlayersTeam}s left.",
            true,
            true,
            3);
        
        [Description("Settings for Camera Scanner (When SCP enters Surface):")]
        public bool IsCameraScannerLookForScpLeavingFeatureEnabled { get; set; } = true;
        public bool ShouldCameraScannerAnnounceScpLeavingOnlyOneTime { get; set; } = true;
        public bool ShouldCassieCheckIfScpIsStillOnSurfaceAfterTheDelay { get; set; } = true;

        public CassieAnnouncement ScpLeavingCassie { get; set; } = new CassieAnnouncement(
            "the camera system has detected {ScpRole} outside the facility at {Gate}",
            "The Camera System has detected {ScpRole} outside the Facility at {Gate}.",
            true,
            true,
            10);
        
        
        [Description("Settings for Camera Scanner (When CI enters the Facility) (Warning! This feature is in the base game now! At the time of making this plugin, there is no way to turn it off. If you want to use both features, from the plugin and the base game, set this to true. Its False by default.):")]
        public bool IsCameraScannerLookingForCiEnteringFeatureEnabled { get; set; } = false;
        [Description("If set to false, cassie wont announce CI untill next CI spawn (option below)")]
        public bool ShouldCameraScannerAnnounceCiEnteringOnlyOneTime { get; set; } = false;
        public CassieAnnouncement CiEnteringCassie { get; set; } = new CassieAnnouncement(
            "the camera system has detected chaos insurgency agents inside the facility at {Gate}",
            "The Camera System has detected Chaos Insurgency Agents inside the Facility at {Gate}.",
            true,
            true,
            10);
        
        
        
        [Description("Settings for Warhead (When someone turns it on):")]
        public bool IsWarheadFeatureEnabled { get; set; } = true;
        public bool IsWarheadAnnouncementTurningOnEnabled { get; set; } = true;
        public bool IsWarheadAnnouncementTurningOffEnabled { get; set; } = true;
        [Description("If set to false, the announcements will be on cooldown specified below. If set to true, cassie will announce it only one time")]
        public bool ShouldWarheadAnnounceOnlyOneTime { get; set; } = false;
        public int WarheadAnnouncementCooldown { get; set; } = 30;
        public CassieAnnouncement WarheadTurningOnCassie { get; set; } = new CassieAnnouncement(
            "Warhead has been turned on by {PlayersTeam}",
            "Warhead has been turned on by {PlayersTeam}.",
            true,
            true,
            3);
        public CassieAnnouncement WarheadTurningOffCassie { get; set; } = new CassieAnnouncement(
            "Warhead has been turned off by {PlayersTeam}",
            "Warhead has been turned off by {PlayersTeam}.",
            true,
            true,
            3);
        
        
        
        [Description("Settings for SCP escape:")]
        public bool IsScpEscapeEnabled { get; set; } = true;
        public string CommandName { get; set; } = "escape";
        public string CommandDescription { get; set; } = "Lets you escape the facility as a SCP, when you're at the escape room";
        public string HintWhenCanEscape { get; set; } = "You can escape by typing .{CommandName} in the console by pressing [`] or [~]!";
        public string EscapeFailedDueToFeatureTurnedOff { get; set; } = "This feature is turned off!";
        public string EscapeFailedDueToPlayerNotBeingScp { get; set; } = "Only SCPs can escape!";
        public string EscapeFailedDueToPlayerNotBeingAtEscape { get; set; } = "You are not in the escape area!";
        public string EscapeSuccess { get; set; } = "You escaped!";
        public RoleTypeId RoleToChange { get; set; } = RoleTypeId.NtfCaptain;
        public SpawnReason SpawnReason { get; set; } = SpawnReason.Escaped;
        public RoleSpawnFlags RoleSpawnFlags { get; set; } = RoleSpawnFlags.All;
        public bool ShouldSentCassieAfterEscape { get; set; } = true;
        public CassieAnnouncement ScpEscapingCassie { get; set; } = new CassieAnnouncement(
            "warning . the camera system has lost information about the location of {ScpRole} . it is possible that there has been an escape",
            "Warning. The camera system has lost information about the location of {ScpRole}. It is possible that there has been an escape.",
            true,
            true,
            15);
    }
}