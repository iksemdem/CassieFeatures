using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using UnityEngine;
using UnityEngine.Serialization;

namespace CassieFeatures
{
    public class ColliderLeavingFacilityTriggerHandler : MonoBehaviour
    {
        // This is for Camera Scanner SCP leaving facility
        [FormerlySerializedAs("ColliderName")] public string colliderName;

        void OnTriggerEnter(Collider other)
        {
            // If not player then return
            if (!other.CompareTag("Player")) return;

            Log.Debug($"{colliderName} entered by player");

            // Getting player that triggered the collider
            if (Player.TryGet(other.gameObject, out Player pl))
            {
                string gate;
                string scpText;

                Log.Debug("Checking if player is scp");
                if (pl.IsScp)
                {
                    Log.Debug("player is scp");
                    
                    // checking if scp was already outside
                    Log.Debug("Checking if scp was already outside");
                    if (Utils.WasScpSpottedOutside)
                    {
                        Log.Debug("Scp was outside");
                        
                        // checking if it should stop the logic
                        if (Plugin.Instance.Config.ShouldCameraScannerAnnounceScpLeavingOnlyOneTime)
                        {
                            Log.Debug("Stopping the logic");
                            return;
                        }
                        
                        Log.Debug("Not stopping the logic");
                        
                    } 
                    else
                    {
                        Log.Debug("Scp was not outside");
                        Log.Debug("Setting that scp was outside");
                        Utils.WasScpSpottedOutside = true;
                    }
                    
                    
                    
                    RoleTypeId playersRole = pl.Role;
                    Log.Debug($"players role is now: {playersRole}");

                    switch (playersRole)
                    {
                        case RoleTypeId.Scp049:
                            scpText = "SCP 0 4 9";
                            break;

                        case RoleTypeId.Scp0492:
                            scpText = "SCP 0 4 9 2";
                            break;

                        case RoleTypeId.Scp096:
                            scpText = "SCP 0 9 6";
                            break;

                        case RoleTypeId.Scp106:
                            scpText = "SCP 1 0 6";
                            break;

                        case RoleTypeId.Scp173:
                            scpText = "SCP 1 7 3";
                            break;

                        case RoleTypeId.Scp3114:
                            scpText = "SCP 3 1 1 4";
                            break;

                        case RoleTypeId.Scp939:
                            scpText = "SCP 9 3 9";
                            break;

                        default:
                            scpText = "unspecified SCP";
                            Log.Error("[CassieFeatures] Unspecified SCP role exited the facility! Report this to the plugin manager");
                            break;
                    }
                    
                    Log.Debug($"scp text is now: {scpText}");

                    switch (colliderName)
                    {
                        case "Collider Gate A Outside":
                            gate = "Gate A";
                            break;
                        case "Collider Gate B Outside":
                            gate = "Gate B";
                            break;
                        default:
                            gate = "Unspecified Gate";
                            Log.Error("SCP entered at unknown gate! Report this to the plugin manager");
                            break;
                    }
                    
                    Log.Debug($"gate is now {gate}");

                    Log.Debug("starting the cassie delay");
                    Timing.CallDelayed(Plugin.Instance.Config.CassieDelaySinceScpLeaving, () =>
                    {
                        if (Plugin.Instance.Config.ShouldCassieCheckIfScpIsStillOnSurfaceAfterTheDelay)
                        {
                            Log.Debug("Checking if scp is still outside");
                            if (pl.Zone != ZoneType.Surface)
                            {
                                Log.Debug("Scp is not on the surface, stopping logic");
                                Utils.WasScpSpottedOutside = false;
                                return;
                            }
                            else
                            {
                                Log.Debug("scp is still outside");
                                Utils.WasScpSpottedOutside = false;
                            }
                            
                        }
                        
                        // checking if scp was outside while the delay was delaying bruh
                        Log.Debug("checking if scp was outside while delay");
                        if (Utils.WasScpSpottedOutside)
                        {
                            Log.Debug("Scp was outside while delay");
                        
                            // checking if it should stop the logic
                            if (Plugin.Instance.Config.ShouldCameraScannerAnnounceScpLeavingOnlyOneTime)
                            {
                                Log.Debug("Stopping the logic");
                                return;
                            }
                        
                            Log.Debug("Not stopping the logic");
                        
                        } 
                        else
                        {
                            Log.Debug("Scp was not outside while delay");
                            Log.Debug("setting that scp was now spotted by cassie");
                            Utils.WasScpSpottedOutside = true;
                        }
                        
                        Utils.ScpOnSurfaceCassie(gate, scpText); 

                    }, Server.Host.GameObject);
                }
                else
                {
                    Log.Debug("player is not scp");
                }
            }
        }
    }
}