using App.Scripts.Game.Blocks;
using App.Scripts.Game.Blocks.Models;
using App.Scripts.Game.Effects;
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
                        BlockAction(block);
                    }
                }
            }
        }
        
        private void SliceBlock(Block block, Vector3 direction)
        {
            float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            block.Slice(sliceAngle);
        }

        private void BlockAction(Block block)
        {
            if (block is Fruit)
            {
                FruitSliced(block);
            }
            else if (block is Bomb)
            {
                BombSliced(block);
            }
            else if (block is Heart)
            {
                HeartSliced(block);
            }
        }

        private void FruitSliced(Block block)
        {
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
        }

        private void HeartSliced(Block block)
        {
            liveBar.AddLive();
        }
    }
}