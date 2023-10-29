using UnityEngine;

namespace App.Scripts.Game.Configs.UI
{
    [CreateAssetMenu(fileName = "ButtonAnimationConfig", menuName = "Configs/UI/ButtonAnimation")]
    public class ButtonAnimationConfig : ScriptableObject
    {
        public float scaleMultiplier = 0.9f;
        public float animationDuration = 0.1f;
        public float initialTint = 1f;
        public float pressedTint = 0.8f;
    }
}