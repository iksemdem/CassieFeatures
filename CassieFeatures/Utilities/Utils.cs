using System.Linq;
using CassieFeatures.Colliders;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace CassieFeatures
{
    public static class Utils
    {
        private static BoxCollider colliderEscape1;
        // This is for Camera Scanner SCP leaving facility
        public static bool WasScpSpottedOutside = false;
        // This is for Camera Scanner CI entering facility
        public static bool WasCiSpottedInside = false;
        // This is for Warhead lever change
        public static bool ActualLeverState = false;
        public static bool IsWarheadOnCooldown = false;
        public static bool WasWarheadAnnounced = false;

        // This is for Tesla's cassie
        public static string ReplacePlaceholders(string input, Team team)
        {
            switch (team)
            {
                case Team.ChaosInsurgency:
                {
                    string teamMembersAlive = Player.Get(Team.ChaosInsurgency).Count().ToString();
                    string teamName = "Chaos Insurgency Agent";
                    return input.Replace("{TeamMembersAlive}", teamMembersAlive).Replace("{PlayersTeam}", teamName);
                }
                case Team.ClassD:
                {
                    string teamMembersAlive = Player.Get(RoleTypeId.ClassD).Count().ToString();
                    string teamName = "Class D Personnel";
                    return input.Replace("{TeamMembersAlive}", teamMembersAlive).Replace("{PlayersTeam}", teamName);
                }
                case Team.FoundationForces:
                {
                    string teamMembersAlive = Player.Get(Team.FoundationForces).Count().ToString();
                    string teamName = "Foundation Forces Personnel";
                    return input.Replace("{TeamMembersAlive}", teamMembersAlive).Replace("{PlayersTeam}", teamName);
                }
                case Team.OtherAlive:
                {
                    string teamMembersAlive = Player.Get(RoleTypeId.Tutorial).Count().ToString();
                    string teamName = "Unspecified Agent";
                    return input.Replace("{TeamMembersAlive}", teamMembersAlive).Replace("{PlayersTeam}", teamName);
                }
                case Team.SCPs:
                {
                    string teamMembersAlive = Player.Get(Team.SCPs).Count().ToString();
                    string teamName = "SCP";
                    return input.Replace("{TeamMembersAlive}", teamMembersAlive).Replace("{PlayersTeam}", teamName);
                }
                case Team.Scientists:
                {
                    string teamMembersAlive = Player.Get(RoleTypeId.Scientist).Count().ToString();
                    string teamName = "Unspecified Agent";
                    return input.Replace("{TeamMembersAlive}", teamMembersAlive).Replace("{PlayersTeam}", teamName);
                }
                default:
                    return input.Replace("{TeamMembersAlive}", "Unknown number")
                        .Replace("{PlayersTeam}", "Unknown Team");
            }
        }

        // this is for Camera Scanner SCP leaving facility
        public static void ScpOnSurfaceCassie(string gate, string scpText)
        {
            Log.Debug("Sending SCP leaving facility cassie");
            string cassieMessage = Plugin.Instance.Config.ScpLeavingCassie.Content;
            string cassieText = Plugin.Instance.Config.ScpLeavingCassie.Subtitles;

            cassieMessage = cassieMessage.Replace("{Gate}", gate).Replace("{ScpRole}", scpText);
            cassieText = cassieText.Replace("{Gate}", gate).Replace("{ScpRole}", scpText);
            
            Log.Debug($"cassie on scp leaving facility: {cassieMessage} , {cassieText}");
            
            Cassie.MessageTranslated($"{cassieMessage}", $"{cassieText}", false,
                Plugin.Instance.Config.ScpLeavingCassie.IsNoisy,
                Plugin.Instance.Config.ScpLeavingCassie.ShowSubtitles);
        }

        // this is for Camera Scanner CI entering facility
        public static void CiInsideCassie(string gate)
        {
            Log.Debug("Sending CI entering facility cassie");
            string cassieMessage = Plugin.Instance.Config.CiEnteringCassie.Content;
            string cassieText = Plugin.Instance.Config.CiEnteringCassie.Subtitles;
            
            cassieMessage = cassieMessage.Replace("{Gate}", gate);
            cassieText = cassieText.Replace("{Gate}", gate);
            
            Log.Debug($"cassie on scp leaving facility: {cassieMessage} , {cassieText}");

            Cassie.MessageTranslated($"{cassieMessage}", $"{cassieText}", false,
                Plugin.Instance.Config.CiEnteringCassie.IsNoisy,
                Plugin.Instance.Config.CiEnteringCassie.ShowSubtitles);
        }

        public static void CreateColliders()
        {
            // =====================
            // === O U T S I D E ===
            // =====================
            
            
            // This is for Colliders which are for Camera Scanner SCP leaving facility
            // the ColliderLeavingFacilityTriggerHandler won't do anything if this is turned off
            if (Plugin.Instance.Config.IsCameraScannerLookForScpLeavingFeatureEnabled)
            {
                Log.Debug("Creating the outside colliders");
                
                // Gate A Outside
                GameObject colliderGateAOutsideObject = new GameObject("ColliderGateAOutsideObject");
                BoxCollider colliderGateAOutside = colliderGateAOutsideObject.AddComponent<BoxCollider>();
                
                colliderGateAOutside.size = new Vector3(1f, 2.4f, 1.4f);
                colliderGateAOutside.isTrigger = true;
                
                // Gate A Outside elevator exit position
                // Surface position is fixed, so I don't attach this to a room
                colliderGateAOutsideObject.transform.position = new Vector3(-10.1f, 1001.15f, 0.5f);

                colliderGateAOutsideObject.AddComponent<ColliderLeavingFacilityTriggerHandler>().colliderName =
                    "Collider Gate A Outside";

                // Gate B Outside
                GameObject colliderGateBOutsideObject = new GameObject("ColliderGateBOutsideObject");
                BoxCollider colliderGateBOutside = colliderGateBOutsideObject.AddComponent<BoxCollider>();
                
                colliderGateBOutside.size = new Vector3(1.4f, 2.41f, 1f);
                colliderGateBOutside.isTrigger = true;
                
                // Gate B Outside elevator exit position
                colliderGateBOutsideObject.transform.position = new Vector3(63.7f, 995.9f, -33f);

                colliderGateBOutsideObject.AddComponent<ColliderLeavingFacilityTriggerHandler>().colliderName =
                    "Collider Gate B Outside";
                
                Log.Debug("Successfully created colliders");
            }
            
            // ===================
            // === I N S I D E ===
            // ===================
            

            // This is for Colliders which are for Camera Scanner CI entering facility
            // the ColliderEnteringFacilityTriggerHandler won't do anything if this is turned off
            if (Plugin.Instance.Config.IsCameraScannerLookingForCiEnteringFeatureEnabled)
            {
                Log.Debug("Creating the inside colliders");
                
                // Gate A Inside
                GameObject colliderGateAInsideObject = new GameObject("ColliderGateAInsideObject");
                BoxCollider colliderGateAInside = colliderGateAInsideObject.AddComponent<BoxCollider>();
                
                colliderGateAInside.size = new Vector3(1f, 2.4f, 1.4f);
                colliderGateAInside.isTrigger = true;
                
                // Gate A Inside elevator exit position
                // Unlike surface, this position isn't fixed so we need to attach a room
                Vector3 gateARoomOffset = new Vector3(-4.65f, 1.15f, -3.8f);
                Vector3 gateAFinalPosition = Room.Get(RoomType.EzGateA).WorldPosition(gateARoomOffset);
                Log.Debug($"gate a inside collider position is {gateAFinalPosition}");
                
                colliderGateAInsideObject.transform.position = gateAFinalPosition;

                colliderGateAInsideObject.AddComponent<ColliderEnteringFacilityTriggerHandler>().colliderName =
                    "Collider Gate A Inside";

                // Gate B Inside
                GameObject colliderGateBInsideObject = new GameObject("ColliderGateBInsideObject");
                BoxCollider colliderGateBInside = colliderGateBInsideObject.AddComponent<BoxCollider>();
                
                colliderGateBInside.size = new Vector3(1.4f, 2.4f, 1f);
                colliderGateBInside.isTrigger = true;
                
                // Gate B Inside elevator exit position
                // Unlike surface, this position isn't fixed so we need to attach a room
                Vector3 gateBRoomOffset = new Vector3(-5.4f, 1.15f, -10f);
                Vector3 gateBFinalPosition = Room.Get(RoomType.EzGateB).WorldPosition(gateBRoomOffset);
                Log.Debug($"gate b inside collider position is {gateBFinalPosition}");
                
                colliderGateBInsideObject.transform.position = gateBFinalPosition;

                colliderGateBInsideObject.AddComponent<ColliderEnteringFacilityTriggerHandler>().colliderName =
                    "Collider Gate B Inside"; 
                
                Log.Debug("Successfully created colliders");
            }
            
            // ===================
            // === E S C A P E ===
            // ===================

            // This is for Collider which is for SCP escaping
            // the ColliderEscapingTriggerHandler won't do anything if this is turned off
            if (Plugin.Instance.Config.IsScpEscapeEnabled)
            {
                Log.Debug("Creating the escape collider");
                
                GameObject colliderEscapeObject = new GameObject("ColliderEscapeObject");
                BoxCollider colliderEscape = colliderEscapeObject.AddComponent<BoxCollider>();

                colliderEscape.size = new Vector3(13f, 4f, 10f);
                colliderEscape.isTrigger = true;

                colliderEscape.transform.position = new Vector3(127.5f, 989.75f, 24f);
                
                colliderEscapeObject.AddComponent<ColliderEscapingTriggerHandler>().colliderName =
                    "Collider Escape";

                colliderEscape1 = colliderEscape;
                
                Log.Debug("Successfully created colliders");
                
            }
        }
        
        
        // this is for SCP escaping
        public static BoxCollider GetEscapeCollider()
        {
            return colliderEscape1;
        }

        public static void ScpEscapedCassie(string scpText)
        {
            string cassieMessage = Plugin.Instance.Config.ScpEscapingCassie.Content;
            string cassieText = Plugin.Instance.Config.ScpEscapingCassie.Subtitles;
                    
            cassieMessage = cassieMessage.Replace("{ScpRole}", scpText);
            cassieText = cassieText.Replace("{ScpRole}", scpText);
                    
            Cassie.MessageTranslated($"{cassieMessage}", $"{cassieText}", false,
                Plugin.Instance.Config.ScpEscapingCassie.IsNoisy,
                Plugin.Instance.Config.ScpEscapingCassie.ShowSubtitles);
        }
        
        // this is for warhead CASSIE
        public static void WarheadCooldown()
        {
            IsWarheadOnCooldown = true;
            Log.Debug("Warhead cooldown started");
            Timing.CallDelayed(Plugin.Instance.Config.WarheadAnnouncementCooldown, () =>
            {
                IsWarheadOnCooldown = false;
                Log.Debug("Warhead cooldown stopped");
            }, Server.Host.GameObject);
        }

        public static void WarheadCassie(bool currentState, Team team)
        {
            if (currentState)
            {
                if (Plugin.Instance.Config.IsWarheadAnnouncementTurningOnEnabled)
                {
                    Log.Debug("Warhead turning on cassie");
                    string cassieMessage = Plugin.Instance.Config.WarheadTurningOnCassie.Content;
                    string cassieText = Plugin.Instance.Config.WarheadTurningOnCassie.Subtitles;
                    
                    cassieMessage = ReplacePlaceholders(cassieMessage, team);
                    cassieText = ReplacePlaceholders(cassieText, team);
                    
                    Log.Debug($"cassie warhead on is: {cassieMessage} , {cassieText}");
                    
                    Timing.CallDelayed(Plugin.Instance.Config.WarheadTurningOnCassie.Delay, () =>
                    {
                        Cassie.MessageTranslated($"{cassieMessage}", $"{cassieText}", false,
                            Plugin.Instance.Config.WarheadTurningOnCassie.IsNoisy,
                            Plugin.Instance.Config.WarheadTurningOnCassie.ShowSubtitles);
                    }, Server.Host.GameObject);
                }
            }
            else
            {
                if (Plugin.Instance.Config.IsWarheadAnnouncementTurningOffEnabled)
                {
                    Log.Debug("Warhead turning off cassie");
                    string cassieMessage = Plugin.Instance.Config.WarheadTurningOffCassie.Content;
                    string cassieText = Plugin.Instance.Config.WarheadTurningOffCassie.Subtitles;
                    
                    cassieMessage = ReplacePlaceholders(cassieMessage, team);
                    cassieText = ReplacePlaceholders(cassieText, team);
                    
                    Log.Debug($"cassie warhead off is: {cassieMessage} , {cassieText}");
                    
                    Timing.CallDelayed(Plugin.Instance.Config.WarheadTurningOffCassie.Delay, () =>
                    {
                        Cassie.MessageTranslated($"{cassieMessage}", $"{cassieText}", false,
                            Plugin.Instance.Config.WarheadTurningOffCassie.IsNoisy,
                            Plugin.Instance.Config.WarheadTurningOffCassie.ShowSubtitles);
                    }, Server.Host.GameObject);
                }
            }
        }
    }
}