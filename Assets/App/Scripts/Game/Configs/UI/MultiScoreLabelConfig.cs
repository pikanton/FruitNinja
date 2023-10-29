using UnityEngine;

namespace App.Scripts.Game.Configs.UI
{
    [CreateAssetMenu(fileName = "MultiScoreLabelConfig", menuName = "Configs/UI/MultiScoreLabel")]
    public class MultiScoreLabelConfig : ScriptableObject
    {
        public float animationDuration = 0.15f;
        public float lifeTime = 1f;
        public string fruitFormat = " фрукта";
        public string multiplayerFormat = "х";
    }
}