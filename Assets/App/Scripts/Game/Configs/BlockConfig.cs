using System.Collections.Generic;
using App.Scripts.Game.Blocks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "BlockConfig", menuName = "Configs/BlockView", order = 0)]
    public class BlockConfig : ScriptableObject
    {
        [SerializeField] private List<BlockProperties> blockProperties = new();
        
        public BlockProperties GetRandomBlockView()
        {
            int randomSpriteIndex = Random.Range(0, blockProperties.Count);
            return blockProperties[randomSpriteIndex];
        }
    }
}