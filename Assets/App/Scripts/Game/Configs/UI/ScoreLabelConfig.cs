using UnityEngine;

namespace App.Scripts.Game.Configs.UI
{
    [CreateAssetMenu(fileName = "ScoreLabelConfig", menuName = "Configs/UI/ScoreLabel")]
    public class ScoreLabelConfig : ScriptableObject
    {
        public float animationDuration = 0.15f;
        public float lifeTime = 1f;
    }
}