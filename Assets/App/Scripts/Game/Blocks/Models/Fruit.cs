using UnityEngine;

namespace App.Scripts.Game.Blocks.Models  
{
    public class Fruit : Block
    {
        [SerializeField] private float immortalityTime;

        private float _spawnTime;

        public override void Initialize(float initialSpeed = 0f, float initialAngle = 0f)
        {
            base.Initialize(initialSpeed, initialAngle);
            _spawnTime = Time.time;
        }

        public override bool Slice(float sliceAngle)
        {
            if (_spawnTime + immortalityTime > Time.time)
                return false;
            base.Slice(sliceAngle);
            return true;
        }

        public void SetImmortalityTime(float time)
        {
            immortalityTime = time;
        }
    }
}