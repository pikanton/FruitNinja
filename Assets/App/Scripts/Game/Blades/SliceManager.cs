using System.Collections;
using App.Scripts.Game.Animations;
using App.Scripts.Game.Blocks;
using App.Scripts.Game.Blocks.Models;
using App.Scripts.Game.SceneManagers;
using App.Scripts.Game.UISystem.Lives;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Game.Blades
{
    public class SliceManager : MonoBehaviour
    {
        [SerializeField] private int scoreAmount = 75;
        [SerializeField] private float minSliceVelocity = 25f;

        [SerializeField] private BlockList spawnedBlocks;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private LiveBar liveBar;
        [SerializeField] public float explosionForce = 15f;
        [SerializeField] private float halfBlockInitialSpeed = 6f;
        [SerializeField] private Block blockPrefab;

        [SerializeField] private FreezeManager freezeManager;
        
        public void CheckBlocksToSlice(Vector3 direction)
        {
            float velocity = direction.magnitude / Time.deltaTime;
            
            if (velocity > minSliceVelocity)
            {
                for (int i = 0; i < spawnedBlocks.spawnedBlocks.Count; i++)
                {
                    Block block = spawnedBlocks.spawnedBlocks[i];
                    Vector3 bladePosition = transform.position;
                    if (block.blockCollider.OnTrigger(bladePosition))
                    {
                        spawnedBlocks.spawnedBlocks.RemoveAt(i);
                        SliceBlock(block, direction);
                        BlockAction(block, direction);
                    }
                }
            }
        }
        
        private void SliceBlock(Block block, Vector3 direction)
        {
            float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            block.Slice(sliceAngle);
        }

        private void BlockAction(Block block, Vector3 direction)
        {
            if (block is Fruit)
            {
                FruitSliced(block, direction);
            }
            else if (block is Bomb)
            {
                BombSliced(block);
            }
            else if (block is Heart)
            {
                HeartSliced(block);
            }
            else if (block is Freezer)
            {
                FreezerSliced(block);
            }
        }

        private void FruitSliced(Block block, Vector3 direction)
        {
            float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            float leftBlockOrthogonalAngle = sliceAngle + 90f;
            float rightBlockOrthogonalAngle = sliceAngle - 90f;
            CreateHalfBlock(blockPrefab, block.blockProperties.blockLeftHalf, leftBlockOrthogonalAngle);
            CreateHalfBlock(blockPrefab, block.blockProperties.blockRightHalf, rightBlockOrthogonalAngle);
            scoreManager.AddScore(block.transform.position, scoreAmount);
        }
        
        private void BombSliced(Block block)
        {
            liveBar.RemoveLive();
            foreach (var spawnedBlock in spawnedBlocks.spawnedBlocks)
            {
                Vector3 blockToBomb = spawnedBlock.transform.position - block.transform.position;
                float distance = blockToBomb.magnitude;
                if (distance > 0)
                {
                    float force = explosionForce / distance;
                    Vector3 acceleration = force * blockToBomb.normalized;
                    spawnedBlock.blockMovement.AddVelocity(acceleration);
                }
            }
            foreach (var halfBlock in spawnedBlocks.halfBLocks)
            {
                Vector3 blockToBomb = halfBlock.transform.position - block.transform.position;
                float distance = blockToBomb.magnitude;
                if (distance > 0)
                {
                    float force = explosionForce / distance;
                    Vector3 acceleration = force * blockToBomb.normalized;
                    halfBlock.blockMovement.AddVelocity(acceleration);
                }
            }
        }

        private void HeartSliced(Block block)
        {
            liveBar.AddLive();
        }

        private void FreezerSliced(Block block)
        {
            freezeManager.Freeze();
            BlockCounter.FreezerIsDestroyed = true;
            
        }
        
        private Block CreateHalfBlock(Block block, Sprite halfBlockSprite, float initialAngle)
        {
            var parentBlockTransform = transform;
            var halfBlock = Instantiate(block, parentBlockTransform.position, Quaternion.identity,
                spawnedBlocks.transform);
            halfBlock.Initialize(halfBlockInitialSpeed, initialAngle);
            halfBlock.blockAnimation.transform.rotation = parentBlockTransform.rotation;
            halfBlock.spriteRenderer.sprite = halfBlockSprite;
            halfBlock.shadowAnimation.Initialize();
            spawnedBlocks.halfBLocks.Add(halfBlock);
            return halfBlock;
        }

        private IEnumerator FreezeBlocks(float freezeDuration)
        {
            float startValue = 0f;
            float endValue = 1f;
    
            float currentAnimationTime = 0;
            while (currentAnimationTime < freezeDuration)
            {
                float progress = currentAnimationTime / freezeDuration;
                currentAnimationTime += Time.unscaledDeltaTime;
                SceneProperties.BlocksTimeScale = Mathf.Lerp(startValue, endValue, progress);
                yield return null;
            }

            SceneProperties.BlocksTimeScale = endValue;
        }
    }
}