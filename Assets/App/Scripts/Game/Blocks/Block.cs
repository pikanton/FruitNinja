using App.Scripts.Game.Configs;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ShadowAnimation shadowAnimation;
        [SerializeField] public BlockSpriteConfig.BlockSprites blockSprites;
        [SerializeField] public BlockMovement blockMovement;
        [SerializeField] public BlockAnimation blockAnimation;
        [SerializeField] public float colliderRadius = 1f;
        [SerializeField] public float lifeTime = 100f;

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