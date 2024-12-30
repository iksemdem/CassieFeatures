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
        
        public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
        {
            var pl = Player.Get(sender);
            BoxCollider escapeCollider = Utils.GetEscapeCollider();
            
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
            
            if (!escapeCollider.bounds.Contains(pl.Position))
            {
                response = Plugin.Instance.Config.EscapeFailedDueToPlayerNotBeingAtEscape;
                return false;
            }

            PlayerDisplay playerDisplay = PlayerDisplay.Get(pl);
            playerDisplay.ClearHint();

            if (Plugin.Instance.Config.ShouldSentCassieAfterEscape)
            {
                Role playersRole = pl.Role;
                string scpText;

                switch (playersRole)
                {
                    case Scp049Role:
                        scpText = "SCP 0 4 9";
                        break;

                    case Scp0492Role:
                        scpText = "SCP 0 4 9 2";
                        break;

                    case Scp096Role:
                        scpText = "SCP 0 9 6";
                        break;

                    case Scp106Role:
                        scpText = "SCP 1 0 6";
                        break;

                    case Scp173Role:
                        scpText = "SCP 1 7 3";
                        break;

                    case Scp3114Role:
                        scpText = "SCP 3 1 1 4";
                        break;

                    case Scp939Role:
                        scpText = "SCP 9 3 9";
                        break;
                        
                    case Scp1507Role:
                        scpText = "SCP 1 5 0 7";
                        break;
                        
                    default:
                        scpText = "unspecified SCP";
                        Log.Error("[CassieFeatures] Unspecified SCP role escaped! Report this to the plugin manager (dc: iksemdem_)");
                        break;
                }
                
                Timing.CallDelayed(Plugin.Instance.Config.ScpEscapingCassie.Delay, () =>
                {
                    Utils.ScpEscapedCassie(scpText);
                        
                }, Server.Host.GameObject);
            }
            
            pl.Role.Set(Plugin.Instance.Config.RoleToChange, Plugin.Instance.Config.SpawnReason, Plugin.Instance.Config.RoleSpawnFlags);
            response = Plugin.Instance.Config.EscapeSuccess;
            return true;
        }
    }
}