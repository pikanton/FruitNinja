using UnityEngine;

namespace App.Scripts.Game.Configs.Effects
{
    [CreateAssetMenu(fileName = "EffectConfig", menuName = "Configs/Effects/Effect")]
    public class EffectConfig : ScriptableObject
    {
        public float lifeTime = 0.5f;
        public float scaleAnimationDuration = 0.1f;
    }
}