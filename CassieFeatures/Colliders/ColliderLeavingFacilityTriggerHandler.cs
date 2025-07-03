using CassieFeatures.Utilities;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using MEC;
using UnityEngine;
using UnityEngine.Serialization;

namespace CassieFeatures.Colliders
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
                    if (EventHandlers.WasScpSpottedOutside)
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
                        EventHandlers.WasScpSpottedOutside = true;
                    }
                    
                    Role playersRole = pl.Role;
                    Log.Debug($"players role is now: {playersRole}");

                    scpText = HandleReplacingPlaceholders.ReplacePlaceholdersScpRole("{ScpRole}", playersRole);
                    
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
                            Log.Error("[CassieFeatures] SCP entered at unknown gate! Report this to the plugin manager");
                            break;
                    }
                    
                    Log.Debug($"gate is now {gate}");

                    Log.Debug("starting the cassie delay");
                    Timing.CallDelayed(Plugin.Instance.Config.ScpLeavingCassie.Delay, () =>
                    {
                        if (Plugin.Instance.Config.ShouldCassieCheckIfScpIsStillOnSurfaceAfterTheDelay)
                        {
                            Log.Debug("Checking if scp is still outside");
                            if (pl.Zone != ZoneType.Surface)
                            {
                                Log.Debug("Scp is not on the surface, stopping logic");
                                EventHandlers.WasScpSpottedOutside = false;
                                return;
                            }
                            else
                            {
                                Log.Debug("scp is still outside");
                                EventHandlers.WasScpSpottedOutside = false;
                            }
                            
                        }
                        
                        // checking if scp was outside while the delay was delaying bruh
                        Log.Debug("checking if scp was outside while delay");
                        if (EventHandlers.WasScpSpottedOutside)
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
                            EventHandlers.WasScpSpottedOutside = true;
                        }
                        
                        HandleCassieAnnouncements.ScpOnSurfaceCassie(gate, scpText); 

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