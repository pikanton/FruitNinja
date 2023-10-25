using System.Collections.Generic;
using App.Scripts.Game.Blocks.Models;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class BlockList : MonoBehaviour
    {
        [SerializeField] public List<Block> spawnedBlocks = new();
    }
}