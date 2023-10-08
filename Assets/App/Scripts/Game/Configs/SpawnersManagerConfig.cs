using App.Scripts.Game.Camera;
using UnityEngine;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "SpawnerManager", menuName = "Configs", order = 0)]
    public class SpawnersManagerConfig : ScriptableObject
    {
        public float destroyAreaScale = 1.4f;
        
        public int minBlockCountInStack = 2;
        public int maxBlockCountInStack = 5;

        public float difficultyIncreaseTime = 5f;
        public float spawnReductionTimeFactor = 0.1f;
        public float stackSpawnTime = 4f;
        public float blockSpawnTime = 0.4f;
        public float startSpawnDelay = 1f;
    }
}