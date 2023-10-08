using UnityEngine;

namespace App.Scripts.Game.Block
{
    public class BlockMovement : MonoBehaviour
    {
        public float initialSpeed = 13f;
        public float gravity = 9.8f;

        private float _initializeAngle;
        private Vector3 _initialVelocity;
        private Vector3 _currentPosition;

        private float _destroyAreaWidth;
        private float _destroyAreaHeight;
        
        private void Start()
        {
            _initializeAngle = GetInitialAngle();
            _initialVelocity = GetInitialVelocity();
            _currentPosition = transform.position;
        }

        private void FixedUpdate()
        {
            UpdatePosition();

            if (IsOutOfScreen())
                Destroy(gameObject);
        }

        private void UpdatePosition()
        {
            _currentPosition += _initialVelocity * Time.deltaTime;
            _initialVelocity.y -= gravity * Time.deltaTime;
          
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
            float initialVelocityX = initialSpeed * Mathf.Cos(radianAngle);
            float initialVelocityY = initialSpeed * Mathf.Sin(radianAngle);
            return new Vector3(initialVelocityX, initialVelocityY, 0f);
        }

        private bool IsOutOfScreen()
        {
            Vector3 position = transform.position;

            float halfDestroyAreaWidth = _destroyAreaWidth / 2f;
            float halfDestroyAreaHeight = _destroyAreaHeight / 2f;

            return position.y < -halfDestroyAreaHeight ||
                   position.y > halfDestroyAreaHeight ||
                   position.x < -halfDestroyAreaWidth ||
                   position.x > halfDestroyAreaWidth;
        }
        
        public void SetDestroyArea(float width, float height)
        {
            _destroyAreaWidth = width;
            _destroyAreaHeight = height;
        }
    }
}