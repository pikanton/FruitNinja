using App.Scripts.Game.Configs.Gameplay;
using App.Scripts.Game.SceneManagers;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class BlockMovement : MonoBehaviour
    {
        [SerializeField] private PhysicsConfig physicsConfig;
        
        private float _initialSpeed;
        private float _initialAngle;
        private Vector3 _initialVelocity;
        private Vector3 _currentPosition;
        
        public void Initialize(float initialSpeed = 0f, float initialAngle = 0f)
        {
            _initialSpeed = initialSpeed;
            _initialAngle = initialAngle;
            _initialVelocity = GetInitialVelocity();
            _currentPosition = transform.position;
        }

        private void Update()
        {
            _currentPosition += _initialVelocity * (Time.deltaTime * SceneProperties.BlocksTimeScale);
            float afterGravityVelocity = _initialVelocity.y - physicsConfig.gravity * Time.deltaTime * SceneProperties.BlocksTimeScale;
            _initialVelocity.y = Mathf.Clamp(afterGravityVelocity, -physicsConfig.velocityLimit,
                physicsConfig.velocityLimit);
            transform.position = _currentPosition;
        }

        private Vector3 GetInitialVelocity()
        {
            float radianAngle = Mathf.Deg2Rad * _initialAngle;
            float initialVelocityX = Mathf.Clamp(_initialSpeed * Mathf.Cos(radianAngle), 
                -physicsConfig.velocityLimit, physicsConfig.velocityLimit);
            float initialVelocityY = Mathf.Clamp(_initialSpeed * Mathf.Sin(radianAngle),
                -physicsConfig.velocityLimit, physicsConfig.velocityLimit);
            return new Vector3(initialVelocityX, initialVelocityY, 0f);
        }

        public void AddVelocity(Vector3 velocity)
        {
            _initialVelocity.x = Mathf.Clamp(_initialVelocity.x + velocity.x, 
                -physicsConfig.velocityLimit, physicsConfig.velocityLimit);
            _initialVelocity.y = Mathf.Clamp(_initialVelocity.y + velocity.y, 
                -physicsConfig.velocityLimit, physicsConfig.velocityLimit);
            _initialVelocity.z = Mathf.Clamp(_initialVelocity.z + velocity.z, 
                -physicsConfig.velocityLimit, physicsConfig.velocityLimit);
        }
    }
}