using App.Scripts.Game.Blocks;
using App.Scripts.Game.Blocks.Models;
using App.Scripts.Game.Configs;
using App.Scripts.Game.Configs.Boosters;
using App.Scripts.Game.Configs.Gameplay;
using App.Scripts.Game.Configs.View;
using App.Scripts.Game.SceneManagers;
using App.Scripts.Game.UISystem.Lives;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;
    
namespace App.Scripts.Game.Blades
{
    public class SliceManager : MonoBehaviour
    {
        [SerializeField] private SliceConfig sliceConfig;
        [SerializeField] private BlockList spawnedBlocks;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private LiveBar liveBar;
        [SerializeField] private Block blockPrefab;

        [SerializeField] private ObjectOrdersConfig objectOrdersConfig;
        [SerializeField] private HalfBlockConfig halfBlockConfig;
        [SerializeField] private BombConfig bombConfig;
        [SerializeField] private FreezeManager freezeManager;
        [SerializeField] private FruitBoxConfig fruitBoxConfig;
        
        public void CheckBlocksToSlice(Vector3 direction)
        {
            float velocity = direction.magnitude / Time.deltaTime;
            
            if (velocity > sliceConfig.minSliceVelocity)
            {
                for (int i = 0; i < spawnedBlocks.spawnedBlocks.Count; i++)
                {
                    Block block = spawnedBlocks.spawnedBlocks[i];
                    Vector3 bladePosition = transform.position;
                    if (block.blockCollider.OnTrigger(bladePosition))
                    {
                        float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
                        if (block.Slice(sliceAngle))
                        {
                            spawnedBlocks.spawnedBlocks.RemoveAt(i);
                            BlockAction(block, direction);
                        }
                    }
                }
            }
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
            else if (block is FruitBox)
            {
                FruitBoxSliced(block, direction);
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
            scoreManager.AddScore(block.transform.position, sliceConfig.scoreAmount);
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
                    float force = bombConfig.explosionForce / distance;
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
                    float force = bombConfig.explosionForce / distance;
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
        
        private void FruitBoxSliced(Block block, Vector3 direction)
        {
            float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            float leftBlockOrthogonalAngle = sliceAngle + 90f;
            float rightBlockOrthogonalAngle = sliceAngle - 90f;
            CreateHalfBlock(blockPrefab, block.blockProperties.blockLeftHalf, leftBlockOrthogonalAngle);
            CreateHalfBlock(blockPrefab, block.blockProperties.blockRightHalf, rightBlockOrthogonalAngle);
            for (int i = 0; i < fruitBoxConfig.spawnFruitCount; i++)
            {
                SpawnFruit(block);
            }
        }
        
        private Block CreateHalfBlock(Block block, Sprite halfBlockSprite, float initialAngle)
        {
            var parentBlockTransform = transform;
            var halfBlock = Instantiate(block, parentBlockTransform.position, Quaternion.identity,
                spawnedBlocks.transform);
            halfBlock.spriteRenderer.sortingOrder = objectOrdersConfig.halfBlockOrder;
            halfBlock.Initialize(halfBlockConfig.halfBlockInitialSpeed, initialAngle);
            halfBlock.blockAnimation.transform.rotation = parentBlockTransform.rotation;
            halfBlock.spriteRenderer.sprite = halfBlockSprite;
            halfBlock.shadowAnimation.Initialize();
            spawnedBlocks.halfBLocks.Add(halfBlock);
            return halfBlock;
        }
        
        private void SpawnFruit(Block fruitBoxBlock)
        {
            Fruit newFruit = Instantiate(fruitBoxConfig.fruitPrefab, fruitBoxBlock.transform.position,
                Quaternion.identity, spawnedBlocks.gameObject.transform);
            newFruit.Initialize(fruitBoxConfig.fruitBoxFruitInitialSpeed, GetRandomAngle());
            newFruit.SetImmortalityTime(fruitBoxConfig.fruitBoxImmortalityTime);
            spawnedBlocks.spawnedBlocks.Add(newFruit);
        }
        
        private float GetRandomAngle()
        {
            float angle;
            if (fruitBoxConfig.firstSpawnAngle > fruitBoxConfig.secondSpawnAngle)
                angle = Random.Range(fruitBoxConfig.firstSpawnAngle, fruitBoxConfig.secondSpawnAngle);
            else
                angle = Random.Range(fruitBoxConfig.firstSpawnAngle, fruitBoxConfig.secondSpawnAngle);
            return angle;
        }
    }
}