using System;
using App.Scripts.Game.Blocks.Models;

namespace App.Scripts.Game.Blocks
{
    [Serializable]
    public class BlockPrefab
    {
        public Block prefab;
        public float probability;
    }
}