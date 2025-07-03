using Exiled.API.Features;
using Exiled.API.Features.Roles;
using MEC;
using PlayerRoles;

namespace CassieFeatures.Utilities
{
    public static class HandleCassieAnnouncements
    {
        
        // ===================================
        // === C A M E R A   S C A N N E R ===
        // ===================================
        
        // this is for Camera Scanner SCP leaving facility CASSIE
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
        
        // this is for Camera Scanner CI entering facility CASSIE
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
        
        // ===============================
        // === S C P   E S C A P I N G ===
        // ===============================
        
        // This is for SCP escaping CASSIE
        public static void ScpEscapedCassie(Role scpRole)
        {
            string cassieMessage = Plugin.Instance.Config.ScpEscapingCassie.Content;
            string cassieText = Plugin.Instance.Config.ScpEscapingCassie.Subtitles;

            cassieMessage = HandleReplacingPlaceholders.ReplacePlaceholdersScpRole(cassieMessage, scpRole);
            cassieText = HandleReplacingPlaceholders.ReplacePlaceholdersScpRole(cassieText, scpRole);
                    
            Cassie.MessageTranslated($"{cassieMessage}", $"{cassieText}", false,
                Plugin.Instance.Config.ScpEscapingCassie.IsNoisy,
                Plugin.Instance.Config.ScpEscapingCassie.ShowSubtitles);
        }
        
        // ===================================
        // === W A R H E A D   C H A N G E ===
        // ===================================
        
        // this is for warhead CASSIE
        public static void WarheadCassie(bool currentState, Team team)
        {
            if (currentState)
            {
                if (Plugin.Instance.Config.IsWarheadAnnouncementTurningOnEnabled)
                {
                    Log.Debug("Warhead turning on cassie");
                    string cassieMessage = Plugin.Instance.Config.WarheadTurningOnCassie.Content;
                    string cassieText = Plugin.Instance.Config.WarheadTurningOnCassie.Subtitles;
                    
                    cassieMessage = HandleReplacingPlaceholders.ReplacePlaceholdersTeam(cassieMessage, team);
                    cassieText = HandleReplacingPlaceholders.ReplacePlaceholdersTeam(cassieText, team);
                    
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
                    
                    cassieMessage = HandleReplacingPlaceholders.ReplacePlaceholdersTeam(cassieMessage, team);
                    cassieText = HandleReplacingPlaceholders.ReplacePlaceholdersTeam(cassieText, team);
                    
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
        
        // =================
        // === T I M E D ===
        // =================

        public static void SendTimedCassies()
        {
            if (Plugin.Instance.Config.CassieAnnouncements == null) return;
            
            foreach (CassieAnnouncement cassie in Plugin.Instance.Config.CassieAnnouncements)
            {
                Timing.CallDelayed(cassie.Delay, () =>
                {
                    Cassie.MessageTranslated(
                        cassie.Content,
                        cassie.Subtitles,
                        false,
                        cassie.IsNoisy,
                        cassie.ShowSubtitles);
                }, Server.Host.GameObject);
            }
        }
        
        // ===================================
        // === E S C A P E   W A R H E A D ===
        // ===================================

        public static void EscapeWarheadCassie(float delay)
        {
            Log.Debug("Sending CI entering facility cassie");
            string cassieMessage = Plugin.Instance.Config.ScpEscapingWarheadCassie.Content;
            string cassieText = Plugin.Instance.Config.ScpEscapingWarheadCassie.Subtitles;
            
            cassieMessage = cassieMessage.Replace("{WarheadDelay}", delay.ToString());
            cassieText = cassieText.Replace("{WarheadDelay}", delay.ToString());
            
            Log.Debug($"cassie on scp leaving facility: {cassieMessage} , {cassieText}");

            Cassie.MessageTranslated($"{cassieMessage}", $"{cassieText}", false,
                Plugin.Instance.Config.ScpEscapingWarheadCassie.IsNoisy,
                Plugin.Instance.Config.ScpEscapingWarheadCassie.ShowSubtitles);
        }
        
    }
}