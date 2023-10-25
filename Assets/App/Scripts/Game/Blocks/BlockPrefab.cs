using System;
using App.Scripts.Game.Blocks.Models;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    [Serializable]
    public class BlockPrefab
    {
        public Block prefab;
        public float probability;
    }
}