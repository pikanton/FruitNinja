﻿using App.Scripts.Game.Blocks;
using App.Scripts.Game.Blocks.Models;
using App.Scripts.Game.Configs.Gameplay;
using App.Scripts.Game.UISystem.Lives;
using UnityEngine;

namespace App.Scripts.Game.SceneManagers
{
    public class BlockDestroyManager : MonoBehaviour
    {
        [SerializeField] private SpawnersManagerConfig managerConfig;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private BlockList blockList;
        [SerializeField] private LiveBar liveBar;
        
        public Rect GetDestroyAreaRect()
        {
            Rect destroyArea = cameraManager.GetCameraRect();
            destroyArea.height *= managerConfig.destroyAreaScale;
            destroyArea.width *= managerConfig.destroyAreaScale;
            destroyArea.center = Vector2.zero;
            return destroyArea;
        }
        
        private void Update()
        {
            DestroyScreenOutBlocks();
        }

        private void DestroyScreenOutBlocks()
        {
            for (int i = 0; i < blockList.spawnedBlocks.Count; i++)
            {
                Block block = blockList.spawnedBlocks[i];
                if (IsOutOfScreen(block.transform))
                {
                    if (block is Freezer)
                        BlockCounter.FreezerCount = 0;
                    Destroy(block.gameObject);
                    blockList.spawnedBlocks.RemoveAt(i);
                    if (block is Fruit)
                    {
                        liveBar.RemoveLive();
                    }
                }
            }

            for (int i = 0; i < blockList.halfBLocks.Count; i++)
            {
                Block block = blockList.halfBLocks[i];
                if (IsOutOfScreen(block.transform))
                {
                    Destroy(block.gameObject);
                    blockList.halfBLocks.RemoveAt(i);
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