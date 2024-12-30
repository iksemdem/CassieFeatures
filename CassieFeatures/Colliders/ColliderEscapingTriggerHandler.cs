using Exiled.API.Features;
using UnityEngine;
using UnityEngine.Serialization;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;

namespace CassieFeatures.Colliders
{
    public class ColliderEscapingTriggerHandler : MonoBehaviour
    {
        [FormerlySerializedAs("ColliderName")] public string colliderName;

        private DynamicHint _currentHint;

        public DynamicHint CurrentHint => _currentHint;
        
        public DynamicHint GetHint()
        {
            return _currentHint;
        }
        void OnTriggerEnter(Collider other)
        {
            // If not player then return
            if (!other.CompareTag("Player")) return;

            Log.Debug($"{colliderName} entered by player");

            // Getting player that triggered the collider
            if (Player.TryGet(other.gameObject, out Player pl))
            {
                if (!pl.IsScp) return;

                string hintContent = Plugin.Instance.Config.HintWhenCanEscape;
                
                hintContent = hintContent.Replace("{CommandName}", Plugin.Instance.Config.CommandName);
                
                _currentHint = new DynamicHint
                {
                    Text = hintContent,
                };
                
                Log.Debug($"{_currentHint}");
                PlayerDisplay playerDisplay = PlayerDisplay.Get(pl);
                playerDisplay.AddHint(_currentHint);
            }
        }

        void OnTriggerExit(Collider other)
        {
            // If not player then return
            if (!other.CompareTag("Player")) return;

            Log.Debug($"{colliderName} exited by player");

            // Getting player that triggered the collider
            if (Player.TryGet(other.gameObject, out Player pl))
            {
                if (!pl.IsScp) return;
                
                PlayerDisplay playerDisplay = PlayerDisplay.Get(pl);

                if (_currentHint == null) return;
                playerDisplay.RemoveHint(_currentHint);
                _currentHint = null;
            }
        }
    }
}