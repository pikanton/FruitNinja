using App.Scripts.Game.Configs;
using App.Scripts.Game.Effects;
using UnityEngine;

namespace App.Scripts.Game.Blocks.Models
{
    public class Block : MonoBehaviour
    {
        [SerializeField] public BlockMovement blockMovement;
        [SerializeField] public ShadowAnimation shadowAnimation;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] public BlockAnimation blockAnimation;
        [SerializeField] public BlockCollider blockCollider;
        [SerializeField] public BlockProperties blockProperties;
        [SerializeField] public Blot blotPrefab;
        [SerializeField] public Juice juicePrefab;
        [SerializeField] private BlockConfig blockConfig;
        

        public virtual void Initialize(float initialSpeed = 0f, float initialAngle = 0f)
        {
            blockProperties = blockConfig.GetRandomBlockView();
            spriteRenderer.sprite = blockProperties.blockSprite;
            InitializeComponents(initialSpeed, initialAngle);
        }

        protected void InitializeComponents(float initialSpeed = 0f, float initialAngle = 0f)
        {
            blockCollider.Initialize(blockProperties.colliderRadius);
            blockMovement.Initialize(initialSpeed, initialAngle);
            blockAnimation.Initialize();
            shadowAnimation.Initialize();
        }
        
        public virtual void Slice(float sliceAngle)
        {
            Transform blockTransform = transform;
            Vector3 blockPosition = blockTransform.position;

            CreateJuiceParticle(blockPosition, blockProperties.juiceColor);
            CreateBlot(blockPosition, blockProperties.blotSprite);
            
            Destroy(gameObject);
        }
        
        
        protected Juice CreateJuiceParticle(Vector3 parentBlockPosition, Color juiceColor)
        {
            var juice = Instantiate(juicePrefab, parentBlockPosition, Quaternion.identity);
            juice.Initialize(juiceColor);
            return juice;
        }

        private Blot CreateBlot(Vector3 parentBlockPosition, Sprite blotSprite)
        {
            var blot = Instantiate(blotPrefab, parentBlockPosition, Quaternion.identity);
            blot.spriteRenderer.sprite = blotSprite;
            blot.Initialize();
            return blot;
        }

    }
}