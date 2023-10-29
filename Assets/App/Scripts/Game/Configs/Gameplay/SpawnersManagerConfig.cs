using UnityEngine;

namespace App.Scripts.Game.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "SpawnerManagerConfig", menuName = "Configs/Gameplay/SpawnerManager")]
    public class SpawnersManagerConfig : ScriptableObject
    {
        public float destroyAreaScale = 1.4f;
        
        public int minBlockCountInStack = 2;
        public int maxBlockCountInStack = 6;

        public float blockInitialSpeed = 13f;
        public float difficultyIncreaseTime = 3f;
        public float spawnReductionTimeFactor = 0.15f;
        public float stackSpawnTime = 4f;
        public float blockSpawnTime = 0.4f;
        public float startSpawnDelay = 1f;
    }
}