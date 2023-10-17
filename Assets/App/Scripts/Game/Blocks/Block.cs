using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ShadowAnimation shadowAnimation;
        [SerializeField] private BlockMovement blockMovement;
        
        [SerializeField] public BlockAnimation blockAnimation;
        [SerializeField] public BlockCollider blockCollider;
        [SerializeField] public BlockProperties blockProperties;
        [SerializeField] public float lifeTime = 100f;


        public void Initialize(float initialSpeed = 0f, float initialAngle = 0f)
        {
            spriteRenderer.sprite = blockProperties.blockSprite;
            blockCollider.Initialize(blockProperties.colliderRadius);
            blockMovement.Initialize(initialSpeed, initialAngle);
            blockAnimation.Initialize();
            shadowAnimation.Initialize();
            Destroy(gameObject, lifeTime);
        }
    }
}