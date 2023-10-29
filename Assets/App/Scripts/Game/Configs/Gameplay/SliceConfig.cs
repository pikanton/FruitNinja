using UnityEngine;

namespace App.Scripts.Game.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "SliceConfig", menuName = "Configs/Gameplay/Slice")]
    public class SliceConfig : ScriptableObject
    {
        public int scoreAmount = 75;
        public float minSliceVelocity = 25f;
    }
}