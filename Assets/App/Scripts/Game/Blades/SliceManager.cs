using App.Scripts.Game.Blocks;
using App.Scripts.Game.Blocks.Models;
using App.Scripts.Game.SceneManagers;
using App.Scripts.Game.UISystem.Lives;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;
    
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
        
        [SerializeField] private Fruit fruitPrefab;
        [SerializeField] private int spawnFruitCount;
        [SerializeField] private float firstSpawnAngle;
        [SerializeField] private float secondSpawnAngle;
        [SerializeField] private float fruitBoxFruitInitialSpeed = 7f;
        [SerializeField] private float fruitBoxImmortalityTime = 0.2f;
        
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
        
        private void FruitBoxSliced(Block block, Vector3 direction)
        {
            float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            float leftBlockOrthogonalAngle = sliceAngle + 90f;
            float rightBlockOrthogonalAngle = sliceAngle - 90f;
            CreateHalfBlock(blockPrefab, block.blockProperties.blockLeftHalf, leftBlockOrthogonalAngle);
            CreateHalfBlock(blockPrefab, block.blockProperties.blockRightHalf, rightBlockOrthogonalAngle);
            for (int i = 0; i < spawnFruitCount; i++)
            {
                SpawnFruit(block);
            }
        }
        
        private Block CreateHalfBlock(Block block, Sprite halfBlockSprite, float initialAngle)
        {
            var parentBlockTransform = transform;
            var halfBlock = Instantiate(block, parentBlockTransform.position, Quaternion.identity,
                spawnedBlocks.transform);
            halfBlock.spriteRenderer.sortingOrder = -100;
            halfBlock.Initialize(halfBlockInitialSpeed, initialAngle);
            halfBlock.blockAnimation.transform.rotation = parentBlockTransform.rotation;
            halfBlock.spriteRenderer.sprite = halfBlockSprite;
            halfBlock.shadowAnimation.Initialize();
            spawnedBlocks.halfBLocks.Add(halfBlock);
            return halfBlock;
        }
        
        private void SpawnFruit(Block fruitBoxBlock)
        {
            Fruit newFruit = Instantiate(fruitPrefab, fruitBoxBlock.transform.position,
                Quaternion.identity, spawnedBlocks.gameObject.transform);
            newFruit.Initialize(fruitBoxFruitInitialSpeed, GetRandomAngle());
            newFruit.SetImmortalityTime(fruitBoxImmortalityTime);
            spawnedBlocks.spawnedBlocks.Add(newFruit);
        }
        
        private float GetRandomAngle()
        {
            float angle;
            if (firstSpawnAngle > secondSpawnAngle)
                angle = Random.Range(firstSpawnAngle, secondSpawnAngle);
            else
                angle = Random.Range(firstSpawnAngle, secondSpawnAngle);
            return angle;
        }
    }
}