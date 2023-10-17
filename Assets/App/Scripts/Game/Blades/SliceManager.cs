using App.Scripts.Game.Blocks;
using App.Scripts.Game.Effects;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;

namespace App.Scripts.Game.Blades
{
    public class SliceManager : MonoBehaviour
    {
        [SerializeField] private int scoreAmount = 75;
        [SerializeField] private float minSliceVelocity = 25f;
        [SerializeField] private float halfBlockLifeTime = 3f;
        [SerializeField] private float halfBlockInitialSpeed = 6f;
        [SerializeField] private BlockList spawnedBlocks;
        [SerializeField] private Block blockPrefab;
        [SerializeField] private Blot blotPrefab;
        [SerializeField] private Juice juicePrefab;
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private ScoreLabel scoreLabel;

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
                    }
                }
            }
        }
        
        private void SliceBlock(Block block, Vector3 direction)
        {
            float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            Transform blockTransform = block.transform;
            Vector3 blockPosition = blockTransform.position;
            BlockProperties blockProperties = block.blockProperties;

            CreateJuiceParticle(blockPosition, blockProperties.juiceColor);
            CreateBlot(blockPosition, blockProperties.blotSprite);
            float leftBlockOrthogonalAngle = sliceAngle + 90f;
            float rightBlockOrthogonalAngle = sliceAngle - 90f;
            CreateHalfBlock(block, blockProperties.blockLeftHalf, leftBlockOrthogonalAngle);
            CreateHalfBlock(block, blockProperties.blockRightHalf, rightBlockOrthogonalAngle);
            AddScore(blockPosition, scoreAmount);
            Destroy(block.gameObject);
        }

        private void AddScore(Vector3 blockPosition, int amount)
        {
            scoreBar.AddScore(amount);
            ScoreLabel newScoreLabel = Instantiate(scoreLabel, blockPosition, Quaternion.identity, scoreBar.transform);
            newScoreLabel.Initialize(amount);
        }
        
        private Juice CreateJuiceParticle(Vector3 parentBlockPosition, Color juiceColor)
        {
            var juice = Instantiate(juicePrefab, parentBlockPosition, Quaternion.identity);
            juice.Initialize(juiceColor);
            return juice;
        }

        private Blot CreateBlot(Vector3 parentBlockPosition, Sprite blotSprite)
        {
            var blot = Instantiate(blotPrefab, parentBlockPosition, Quaternion.identity);
            blot.spriteRenderer.sprite = blotSprite;
            blot.Initialize();
            return blot;
        }
        
        private Block CreateHalfBlock(Block parentBlock, Sprite halfBlockSprite, float initialAngle)
        {
            var parentBlockTransform = parentBlock.transform;
            var halfBlock = Instantiate(blockPrefab, parentBlockTransform.position, Quaternion.identity);
            halfBlock.blockAnimation.transform.rotation = parentBlockTransform.rotation;
            halfBlock.blockProperties.blockSprite = halfBlockSprite;
            halfBlock.lifeTime = halfBlockLifeTime;
            halfBlock.Initialize(halfBlockInitialSpeed, initialAngle);
            return halfBlock;
        }
    }
}