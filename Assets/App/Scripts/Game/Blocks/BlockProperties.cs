using System;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    [Serializable]
    public class BlockProperties
    {
        public Sprite blockSprite;
        public Sprite blockLeftHalf;
        public Sprite blockRightHalf;
        public Sprite blotSprite;
        public Color juiceColor;
        public float colliderRadius;
    }
}