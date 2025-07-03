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
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            Log.Debug($"{colliderName} entered by player");
            
            if (Player.TryGet(other.gameObject, out Player pl))
            {
                if (!pl.IsScp) return;

                string hintContent = Plugin.Instance.Config.HintWhenCanEscape;
                
                hintContent = hintContent.Replace("{CommandName}", Plugin.Instance.Config.CommandName);
                
                _currentHint = new DynamicHint
                {
                    Text = hintContent,
                };
                
                Log.Debug($"{_currentHint.Text}");
                PlayerDisplay playerDisplay = PlayerDisplay.Get(pl);
                playerDisplay.AddHint(_currentHint);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            Log.Debug($"{colliderName} exited by player");
            
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