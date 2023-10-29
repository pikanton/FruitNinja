using System.Collections.Generic;
using App.Scripts.Game.Blocks;
using App.Scripts.Game.Blocks.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "BlockPrefabsManagerConfig", menuName = "Configs/BlockPrefabsManager")]
    public class BlockPrefabsManagerConfig : ScriptableObject
    {
        [SerializeField] private int maxFreezersOnScreen = 1;
        [SerializeField] private List<BlockPrefab> blockPrefabs = new();
        
        public BlockPrefab GetRandomBlockPrefab()
        {
            float sumOfProbabilities = 0f;
            for (int i = 0; i < blockPrefabs.Count; i++)
            {
                sumOfProbabilities += GetBlockProbability(blockPrefabs[i]);
            }

            float randomProbabilityOfSpawner = Random.Range(0f, sumOfProbabilities);
            
            for (int i = 0; i < blockPrefabs.Count; i++)
            {
                randomProbabilityOfSpawner -= GetBlockProbability(blockPrefabs[i]);
                if (randomProbabilityOfSpawner <= 0)
                    return blockPrefabs[i];
            }

            return null;
        }

        private float GetBlockProbability(BlockPrefab blockPrefab)
        {
            if (blockPrefab.prefab is Freezer && BlockCounter.FreezerCount >= maxFreezersOnScreen)
            {
                return 0f;
            }
            return blockPrefab.probability;
        }
    }
}