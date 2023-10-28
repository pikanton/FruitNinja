using UnityEngine;

namespace App.Scripts.Game.Blocks.Models
{
    public class FruitBox : Block
    {
        public override void Initialize(float initialSpeed = 0f, float initialAngle = 0f)
        {
            InitializeComponents(initialSpeed, initialAngle);
        }

        public override bool Slice(float sliceAngle)
        {
            Transform blockTransform = transform;
            Vector3 blockPosition = blockTransform.position;

            CreateJuiceParticle(blockPosition, blockProperties.juiceColor);
            Destroy(gameObject);
            return true;
        }
    }
}