using System;
using App.Scripts.Game.InputSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Game.Blades
{
    public class BladeMovement : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private TrailRenderer trail;
        [SerializeField] private SliceManager sliceManager;
        
        private Vector3 _direction;
        private bool _isSlicing;
        private IInput _inputController;

        public void Initialize(IInput inputController)
        {
            _inputController = inputController;
        }
        
        private void Update()
        {
            if (_inputController.GetPress())
            {
                StartSlicing();
            }
            else if (_inputController.GetUp())
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
            transform.position = newPosition;
            sliceManager.CheckBlocksToSlice(_direction);
        }

        private Vector3 GetNewPosition()
        {
            Vector3 newPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            return newPosition;
        }

        private void OnEnable()
        {
            StopSlicing();
        }
    }
}