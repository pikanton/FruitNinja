using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "BlockSpriteConfig", menuName = "Configs/BlockView", order = 0)]
    public class BlockSpriteConfig : ScriptableObject
    {
        [SerializeField] private List<BlockSprites> blockSprites = new();
        
        public BlockSprites GetRandomBlockView()
        {
            int randomSpriteIndex = Random.Range(0, blockSprites.Count);
            return blockSprites[randomSpriteIndex];
        }
        [Serializable]
        public class BlockSprites
        {
            public Sprite blockSprite;
            public Sprite blockLeftHalf;
            public Sprite blockRightHalf;
            public Sprite blotSprite;
            public Color juiceColor;
        }
    }
}