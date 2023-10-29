using UnityEngine;

namespace App.Scripts.Game.Configs.UI
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Configs/UI/Score")]
    public class ScoreConfig : ScriptableObject
    {
        public float scoreAnimationDuration = 0.5f;
        public string prefix = "Лучший: ";
        public float multiplyScoreDelay = 0.2f;
        public int maxScoreMultiplayer = 4;
    }
}