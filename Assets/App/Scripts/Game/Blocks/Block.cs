using App.Scripts.Game.Configs;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class Block : MonoBehaviour
    {
        public BlockSpriteConfig.BlockSprites blockSprites;
        public SpriteRenderer spriteRenderer;
        public BlockMovement blockMovement;
        public BlockAnimation blockAnimation;
        public ShadowAnimation shadowAnimation;
        public float colliderRadius = 1f;
        public float lifeTime = 100f;

        public void Initialize()
        {
            Destroy(gameObject, lifeTime);
            spriteRenderer.sprite = blockSprites.blockSprite;
            blockMovement.Initialize();
            blockAnimation.Initialize();
            shadowAnimation.Initialize();
        }
    }
}