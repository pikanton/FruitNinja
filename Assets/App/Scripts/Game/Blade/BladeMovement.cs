using UnityEngine;

namespace App.Scripts.Game.Blade
{
    public class BladeMovement : MonoBehaviour
    {
        public Camera camera;
        public TrailRenderer trail;
        public float minSliceVelocity = 0.01f;
        
        private Vector3 _direction;
        private bool _isSlicing;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartSlicing();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopSlicing();
            }
            else if (_isSlicing)
            {
                ContinueSlicing();
            }
        }

        private void StartSlicing()
        {
            Vector3 newPosition = GetNewPosition();
            transform.position = newPosition;
            trail.Clear();
            
            _isSlicing = true;
        }

        private void StopSlicing()
        {
            _isSlicing = false;
        }

        private void ContinueSlicing()
        {
            Vector3 newPosition = GetNewPosition();
            
            _direction = newPosition - transform.position;

            float velocity = _direction.magnitude / Time.deltaTime;

            transform.position = newPosition;
            
            if (velocity > minSliceVelocity)
            {
                //сделать обработку уничтожения блоков
            }
        }

        private Vector3 GetNewPosition()
        {
            Vector3 newPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            return newPosition;
        }
    }
}