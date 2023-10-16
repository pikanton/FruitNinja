using App.Scripts.Game.Blocks;
using App.Scripts.Game.InputSystem;
using UnityEngine;

namespace App.Scripts.Game.Controllers
{
    public class BladeController : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private TrailRenderer trail;
        [SerializeField] private float minSliceVelocity = 0.01f;
        [SerializeField] private BlockList spawnedBlocks;
        [SerializeField] private BlockDestroyController blockDestroyController;
        
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
            float velocity = _direction.magnitude / Time.deltaTime;
            transform.position = newPosition;
            
            if (velocity > minSliceVelocity)
            {
                for (int i = 0; i < spawnedBlocks.blockList.Count; i++)
                {
                    Block block = spawnedBlocks.blockList[i];
                    Vector3 blockPosition = block.transform.position;
                    Vector3 bladePosition = transform.position;
                    if (Vector3.Distance(blockPosition, bladePosition) < block.colliderRadius)
                    {
                        spawnedBlocks.blockList.RemoveAt(i);
                        blockDestroyController.SliceBlock(block, _direction);
                    }
                }
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