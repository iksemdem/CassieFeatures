using System.Collections.Generic;
using System.ComponentModel;
using CassieFeatures.Utilities;
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
        [Description("Number of SCPs escaped after which Alpha Warhead will be started (0 to disable)")]
        public int EscapesToStartWarhead { get; set; } = 2;
        public bool CanWarheadBeStopped { get; set; } = true;
        public int WarheadDelaySinceEscape { get; set; } = 60;
        public CassieAnnouncement ScpEscapingWarheadCassie { get; set; } = new CassieAnnouncement(
            "warning . the camera system has detected a surge of SCP escapes . the emergency alpha warhead detonation will start in t minus {WarheadDelay} seconds",
            "Warning. The camera system has detected a surge of SCP escapes. The emergency Alpha Warhead detonation will start in t minus {WarheadDelay} seconds",
            true,
            true,
            15);
        public CassieAnnouncement ScpEscapingCassie { get; set; } = new CassieAnnouncement(
            "warning . the camera system has lost information about the location of {ScpRole} . it is possible that there has been an escape",
            "Warning. The camera system has lost information about the location of {ScpRole}. It is possible that there has been an escape.",
            true,
            true,
            15);

        
        
        [Description("Settings for Door Locker:")]
        public bool IsLockingDoorsEnabled { get; set; } = true;
        [Description("LockedDoors are doors that are locked at the start of the round, use DoorsAction to open/unlock/destroy doors")]
        public List<DoorType> LockedDoors { get; set; } = new List<DoorType>
        {
            DoorType.PrisonDoor,
            DoorType.CheckpointLczA,
            DoorType.CheckpointLczB,
        };
        public List<LockedDoor> DoorsAction { get; set; } = new List<LockedDoor>
        {
            new LockedDoor(
                DoorType.PrisonDoor,
                20,
                true,
                true,
                false,
                false),
            new LockedDoor(
                DoorType.CheckpointLczA,
                60,
                false,
                true,
                false,
                false),
            new LockedDoor(
            DoorType.CheckpointLczB,
            60,
            false,
            true,
            false,
            false),
        };

        [Description("Here you can put your timed CASSIEs, delay starts at the start of the round.")]
        public List<CassieAnnouncement> CassieAnnouncements { get; set; } = new List<CassieAnnouncement>
        {
            new CassieAnnouncement(
                "attention all personnel . cassie has lost control of the door controlling system",
                "Attention all personnel. C.A.S.S.I.E. has lost control of the Door Controlling System",
                true,
                true,
                10),
        };
    }
}