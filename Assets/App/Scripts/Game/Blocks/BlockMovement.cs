using App.Scripts.Game.Configs;
using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class BlockMovement : MonoBehaviour
    {
        [SerializeField] private PhysicsConfig physicsConfig;
        public float InitialSpeed { get; set; }
        public float InitialAngle { get; set; }
        
        private Vector3 _initialVelocity;
        private Vector3 _currentPosition;
        
        public void Initialize()
        {
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
            float radianAngle = Mathf.Deg2Rad * InitialAngle;
            float initialVelocityX = InitialSpeed * Mathf.Cos(radianAngle);
            float initialVelocityY = InitialSpeed * Mathf.Sin(radianAngle);
            return new Vector3(initialVelocityX, initialVelocityY, 0f);
        }
    }
}