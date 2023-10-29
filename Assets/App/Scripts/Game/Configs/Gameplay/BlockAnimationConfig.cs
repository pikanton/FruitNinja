using UnityEngine;

namespace App.Scripts.Game.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "BlockAnimationConfig", menuName = "Configs/Gameplay/BlockAnimation")]
    public class BlockAnimationConfig : ScriptableObject
    {
        public float rotateAngle = 120f;
        public float targetIncreaseScale = 1.3f;
        public float targetDecreaseScale = 0.8f;
        public float scalingDuration = 3f;
    }
}