using App.Scripts.Game.Effects;
using UnityEngine;

namespace App.Scripts.Game.Blocks.Models    
{
    public class Heart : Block
    {
        [SerializeField] private Effect effectPrefab;
        public override void Initialize(float initialSpeed = 0f, float initialAngle = 0f)
        {
            InitializeComponents(initialSpeed, initialAngle);
        }

        public override bool Slice(float sliceAngle)
        {
            Transform blockTransform = transform;
            Vector3 blockPosition = blockTransform.position;

            CreateJuiceParticle(blockPosition, blockProperties.juiceColor);
            CreateEffect(blockPosition, blockProperties.effectSprite);
            Destroy(gameObject);
            return true;
        }

        private Effect CreateEffect(Vector3 parentBlockPosition, Sprite effectSprite)
        {
            var effect = Instantiate(effectPrefab, parentBlockPosition, Quaternion.identity);
            effect.spriteRenderer.sprite = effectSprite;
            effect.Initialize();
            return effect;
        }
    }
}