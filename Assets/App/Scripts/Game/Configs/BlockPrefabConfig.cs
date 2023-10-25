using System.Collections.Generic;
using App.Scripts.Game.Blocks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "BlockPrefabConfig", menuName = "Configs/BlockPrefabs")]
    public class BlockPrefabConfig : ScriptableObject
    {
        [SerializeField] private List<BlockPrefab> blockPrefabs = new();
        
        public BlockPrefab GetRandomBlockPrefab()
        {
            float sumOfProbabilities = 0f;
            for (int i = 0; i < blockPrefabs.Count; i++)
            {
                sumOfProbabilities += blockPrefabs[i].probability;
            }

            float randomProbabilityOfSpawner = Random.Range(0f, sumOfProbabilities);
            
            for (int i = 0; i < blockPrefabs.Count; i++)
            {
                randomProbabilityOfSpawner -= blockPrefabs[i].probability;
                if (randomProbabilityOfSpawner <= 0)
                    return blockPrefabs[i];
            }

            return null;
        }
    }
}