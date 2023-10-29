using UnityEngine;

namespace App.Scripts.Game.Configs.Boosters
{
    [CreateAssetMenu(fileName = "FreezerConfig", menuName = "Configs/Boosters/Freezer")]
    public class FreezerConfig : ScriptableObject
    {
        public float startFreezeValue;
        public float endFreezeValue = 1f;
        public float freezeDuration = 4f;
        public float startFreezeImageAlpha = 0.7f;
        public float endFreezeImageAlpha;
    }
}