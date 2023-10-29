using UnityEngine;

namespace App.Scripts.Game.Configs.UI
{
    [CreateAssetMenu(fileName = "LivesConfig", menuName = "Configs/UI/Lives")]
    public class LivesConfig : ScriptableObject
    {
        [SerializeField] public float animationDuration = 0.2f;
        [SerializeField] [Range(0, 7)] public int liveCount = 5;
    }
}