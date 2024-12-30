using Exiled.API.Features;
using MEC;
using PlayerRoles;
using UnityEngine;
using UnityEngine.Serialization;

namespace CassieFeatures.Colliders
{
    public class ColliderEnteringFacilityTriggerHandler : MonoBehaviour
    {
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
                
                RoleTypeId playersRole = pl.Role;
                Team playersTeam = playersRole.GetTeam();
                
                Log.Debug("checking if player is ci");
                if (playersTeam is Team.ChaosInsurgency)
                {
                    Log.Debug("player is ci");
                    
                    // checking if ci was already inside
                    Log.Debug("checking if ci was already inside");
                    if (Utils.WasCiSpottedInside)
                    {
                        Log.Debug("ci was inside");
                        Log.Debug("stopping the logic");
                        return;
                    }
                    else
                    {
                        Log.Debug("ci was not inside");
                    }
                    
                    // setting that ci was spotted
                    Log.Debug("setting that ci was inside");
                    Utils.WasCiSpottedInside = true;

                    switch (colliderName)
                    {
                        case "Collider Gate A Inside":
                            gate = "Gate A";
                            break;
                        case "Collider Gate B Inside":
                            gate = "Gate B";
                            break;
                        default:
                            gate = "Unspecified Gate";
                            Log.Error("[CassieFeatures] CI entered at unknown gate! Report this to the plugin manager");
                            break;
                    }
                    
                    Log.Debug($"gate is now {gate}");

                    Log.Debug("starting the cassie delay");
                    Timing.CallDelayed(Plugin.Instance.Config.CiEnteringCassie.Delay, () =>
                    {
                        Utils.CiInsideCassie(gate);
                        
                    }, Server.Host.GameObject);
                }
                else
                {
                    Log.Debug("player is not ci");
                }
            }
        }
    }
}