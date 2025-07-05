using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using HintServiceMeow.Core.Utilities;
using MEC;
using UnityEngine;

namespace CassieFeatures.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class EscapeCommand : ICommand
    {
        public string Command { get; } = Plugin.Instance.Config.CommandName;
        public string[] Aliases { get; } = new string[] {"esc"};
        public string Description { get; } = Plugin.Instance.Config.CommandDescription;
        public int SCPsEscaped = 0;
        public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
        {
            var pl = Player.Get(sender);
            BoxCollider escapeCollider = Utilities.HandleCreatingColliders.GetEscapeCollider();
            
            if (!Plugin.Instance.Config.IsScpEscapeEnabled)
            {
                response = Plugin.Instance.Config.EscapeFailedDueToFeatureTurnedOff;
                return false;
            }
            if (!pl.IsScp)
            {
                response = Plugin.Instance.Config.EscapeFailedDueToPlayerNotBeingScp;
                return false;
            }
            
            Vector3 colliderCenter = escapeCollider.bounds.center;
            float allowedDistance = escapeCollider.bounds.extents.magnitude;
            if (Vector3.Distance(pl.Position, colliderCenter) > allowedDistance)
            {
                response = Plugin.Instance.Config.EscapeFailedDueToPlayerNotBeingAtEscape;
                return false;
            }

            PlayerDisplay playerDisplay = PlayerDisplay.Get(pl);
            playerDisplay.ClearHint();
            
            SCPsEscaped += 1;
            if (SCPsEscaped >= Plugin.Instance.Config.EscapesToStartWarhead && Plugin.Instance.Config.EscapesToStartWarhead != 0 && !Warhead.IsInProgress)
            {
                Utilities.HandleCassieAnnouncements.EscapeWarheadCassie(Plugin.Instance.Config.WarheadDelaySinceEscape);
                
                Timing.CallDelayed(Plugin.Instance.Config.WarheadDelaySinceEscape, () =>
                {
                    if (!Plugin.Instance.Config.CanWarheadBeStopped)
                    {
                        Log.Debug("Starting deadman sequence");
                        DeadmanSwitch.InitiateProtocol();
                    }
                    else
                    {
                        Log.Debug("Starting warhead sequence");
                        Warhead.Start();
                    }
                }, Server.Host.GameObject);
            }
            else
            {
                if (Plugin.Instance.Config.ShouldSentCassieAfterEscape)
                {
                    Role role = pl.Role;
                    Timing.CallDelayed(Plugin.Instance.Config.ScpEscapingCassie.Delay, () =>
                    {
                        Utilities.HandleCassieAnnouncements.ScpEscapedCassie(role);
                    
                    }, Server.Host.GameObject);
                }
            }
            
            pl.Role.Set(Plugin.Instance.Config.RoleToChange, Plugin.Instance.Config.SpawnReason, Plugin.Instance.Config.RoleSpawnFlags);
            
            response = Plugin.Instance.Config.EscapeSuccess;
            return true;
        }
    }
}