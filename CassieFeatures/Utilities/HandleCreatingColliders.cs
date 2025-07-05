using CassieFeatures.Colliders;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;

namespace CassieFeatures.Utilities
{
    public static class HandleCreatingColliders
    {
        private static BoxCollider _colliderEscape;
        
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
                
                // This does not work, ill leave it commented tho
                //colliderGateAOutsideObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                
                colliderGateAOutside.size = new Vector3(1f, 0.3f, 1.4f);
                colliderGateAOutside.isTrigger = true;
                
                // Gate A Outside elevator exit position
                // Surface position is fixed, so I don't attach this to a room
                colliderGateAOutsideObject.transform.position = new Vector3(-10.1f, 300.1f, 0.5f);

                colliderGateAOutsideObject.AddComponent<ColliderLeavingFacilityTriggerHandler>().colliderName =
                    "Collider Gate A Outside";

                // Gate B Outside
                GameObject colliderGateBOutsideObject = new GameObject("ColliderGateBOutsideObject");
                BoxCollider colliderGateBOutside = colliderGateBOutsideObject.AddComponent<BoxCollider>();
                
                // This does not work, ill leave it commented tho
                //colliderGateBOutsideObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                
                colliderGateBOutside.size = new Vector3(1.4f, 0.3f, 1f);
                colliderGateBOutside.isTrigger = true;
                
                // Gate B Outside elevator exit position
                colliderGateBOutsideObject.transform.position = new Vector3(63.7f, 294.9f, -33f);

                colliderGateBOutsideObject.AddComponent<ColliderLeavingFacilityTriggerHandler>().colliderName =
                    "Collider Gate B Outside";
                
                Log.Debug("Successfully created Outside colliders");
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
                
                // This does not work, ill leave it commented tho
                //colliderGateAInsideObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                
                colliderGateAInside.size = new Vector3(1f, 0.3f, 1.4f);
                colliderGateAInside.isTrigger = true;
                
                // Gate A Inside elevator exit position
                // Unlike surface, this position isn't fixed so we need to attach a room
                Vector3 gateARoomOffset = new Vector3(-4.65f, 0.1f, -3.8f);
                Vector3 gateAFinalPosition = Room.Get(RoomType.EzGateA).WorldPosition(gateARoomOffset);
                Log.Debug($"gate a inside collider position is {gateAFinalPosition}");
                
                colliderGateAInsideObject.transform.position = gateAFinalPosition;

                colliderGateAInsideObject.AddComponent<ColliderEnteringFacilityTriggerHandler>().colliderName =
                    "Collider Gate A Inside";

                // Gate B Inside
                GameObject colliderGateBInsideObject = new GameObject("ColliderGateBInsideObject");
                BoxCollider colliderGateBInside = colliderGateBInsideObject.AddComponent<BoxCollider>();
                
                // This does not work, ill leave it commented tho
                //colliderGateBInsideObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                
                colliderGateBInside.size = new Vector3(1.4f, 0.3f, 1f);
                colliderGateBInside.isTrigger = true;
                
                // Gate B Inside elevator exit position
                // Unlike surface, this position isn't fixed so we need to attach a room
                Vector3 gateBRoomOffset = new Vector3(-5.4f, 0.1f, -10f);
                Vector3 gateBFinalPosition = Room.Get(RoomType.EzGateB).WorldPosition(gateBRoomOffset);
                Log.Debug($"gate b inside collider position is {gateBFinalPosition}");
                
                colliderGateBInsideObject.transform.position = gateBFinalPosition;

                colliderGateBInsideObject.AddComponent<ColliderEnteringFacilityTriggerHandler>().colliderName =
                    "Collider Gate B Inside"; 
                
                Log.Debug("Successfully created Inside colliders");
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

                // This does not work, ill leave it commented tho
                //colliderEscapeObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                
                colliderEscape.size = new Vector3(13f, 0.3f, 10f);
                colliderEscape.isTrigger = true;

                colliderEscape.transform.position = new Vector3(127.5f, 288.2f, 24f);
                
                colliderEscapeObject.AddComponent<ColliderEscapingTriggerHandler>().colliderName =
                    "Collider Escape";

                _colliderEscape = colliderEscape;
                
                Log.Debug("Successfully created Escape collider");
                
            }
            Log.Debug("Successfully created all colliders");
        }
        
        // this is for SCP escaping
        public static BoxCollider GetEscapeCollider()
        {
            return _colliderEscape;
        }
    }
}