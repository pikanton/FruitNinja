using App.Scripts.Game.Blocks;
using App.Scripts.Game.Effects;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "SliceConfig", menuName = "Configs/Slice")]

    public class SliceConfig : ScriptableObject
    {
        [SerializeField] private int scoreAmount = 75;
        [SerializeField] private float minSliceVelocity = 25f;
        [SerializeField] private float halfBlockLifeTime = 3f;
        [SerializeField] private float halfBlockInitialSpeed = 6f;
        [SerializeField] private Block blockPrefab;
        [SerializeField] private Blot blotPrefab;
        [SerializeField] private Juice juicePrefab;
        [SerializeField] private ScoreManager scoreManager;
    }
}