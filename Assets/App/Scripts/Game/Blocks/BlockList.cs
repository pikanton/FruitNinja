using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class BlockList : MonoBehaviour
    {
        [SerializeField] public List<Block> blockList = new();
    }
}