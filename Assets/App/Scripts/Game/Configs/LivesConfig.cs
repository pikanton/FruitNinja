using UnityEngine;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "LivesConfig", menuName = "Configs/Lives")]
    public class LivesConfig : ScriptableObject
    {
        [SerializeField] public float animationDuration = 0.2f;
        [SerializeField] [Range(0, 7)] public int liveCount = 5;
    }
}