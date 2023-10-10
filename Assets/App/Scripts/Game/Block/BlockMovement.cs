using App.Scripts.Game.Configs;
using UnityEngine;

namespace App.Scripts.Game.Block
{
    public class BlockMovement : MonoBehaviour
    {
        public PhysicsConfig physicsConfig;

        private float _initializeAngle;
        private Vector3 _initialVelocity;
        private Vector3 _currentPosition;
        
        private void Start()
        {
            _initializeAngle = GetInitialAngle();
            _initialVelocity = GetInitialVelocity();
            _currentPosition = transform.position;
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _currentPosition += _initialVelocity * Time.deltaTime;
            _initialVelocity.y -= physicsConfig.gravity * Time.deltaTime;
          
            transform.position = _currentPosition;
        }
        
        private float GetInitialAngle()
        {
            Vector3 rotationEulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            float initialAngle = rotationEulerAngles.z;
            return initialAngle;
        }

        private Vector3 GetInitialVelocity()
        {
            float radianAngle = Mathf.Deg2Rad * _initializeAngle;
            float initialVelocityX = physicsConfig.initialSpeed * Mathf.Cos(radianAngle);
            float initialVelocityY = physicsConfig.initialSpeed * Mathf.Sin(radianAngle);
            return new Vector3(initialVelocityX, initialVelocityY, 0f);
        }
    }
}