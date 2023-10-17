using App.Scripts.Game.Configs;
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
            _currentPosition += _initialVelocity * Time.deltaTime;
            _initialVelocity.y -= physicsConfig.gravity * Time.deltaTime;
          
            transform.position = _currentPosition;
        }

        private Vector3 GetInitialVelocity()
        {
            float radianAngle = Mathf.Deg2Rad * _initialAngle;
            float initialVelocityX = _initialSpeed * Mathf.Cos(radianAngle);
            float initialVelocityY = _initialSpeed * Mathf.Sin(radianAngle);
            return new Vector3(initialVelocityX, initialVelocityY, 0f);
        }
    }
}