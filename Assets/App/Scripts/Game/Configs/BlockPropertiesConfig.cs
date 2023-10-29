using System.Collections.Generic;
using App.Scripts.Game.Blocks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "BlockPropertiesConfig", menuName = "Configs/BlockProperties")]
    public class BlockPropertiesConfig : ScriptableObject
    {
        [SerializeField] private List<BlockProperties> blockProperties = new();
        
        public BlockProperties GetRandomBlockView()
        {
            int randomSpriteIndex = Random.Range(0, blockProperties.Count);
            return blockProperties[randomSpriteIndex];
        }
    }
}