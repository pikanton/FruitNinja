using System;
using UnityEngine;

namespace App.Scripts.Game.Spawners
{
    public class BlockDestroyer : MonoBehaviour
    {
        public SpawnersManager spawnersManager;

        public Rect GetDestroyAreaRect()
        {
            Rect destroyArea = spawnersManager.cameraManager.GetCameraRect();
            destroyArea.height *= spawnersManager.managerConfig.destroyAreaScale;
            destroyArea.width *= spawnersManager.managerConfig.destroyAreaScale;
            destroyArea.center = Vector2.zero;
            return destroyArea;
        }
        
        private void Update()
        {
            DestroyBlocks();
        }

        private void DestroyBlocks()
        {
            for (int i = 0; i < spawnersManager.spawnedBlocks.Count; i++)
            {
                GameObject block = spawnersManager.spawnedBlocks[i];
                if (IsOutOfScreen(block.transform))
                {
                    Destroy(block);
                    spawnersManager.spawnedBlocks.RemoveAt(i);
                }
            }
        }

        private bool IsOutOfScreen(Transform blockTransform)
        {
            Vector3 position = blockTransform.position;
            Rect destroyArea = GetDestroyAreaRect();
            
            return !destroyArea.Contains(position);
        }
        
    }
}