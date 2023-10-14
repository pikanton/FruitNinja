using App.Scripts.Game.Blocks;
using App.Scripts.Game.Effects;
using App.Scripts.Game.Configs;
using App.Scripts.Game.Spawners;
using UnityEngine;

namespace App.Scripts.Game.Controllers
{
    public class BlockDestroyController : MonoBehaviour
    {
        public float halfBlockInitialSpeed = 6f;
        public float halfBlockLifeTime = 3f;
        public SpawnersController spawnersController;
        public Block blockPrefab;
        public Blot blotPrefab;
        public Juice juicePrefab;
        public void SliceBlock(Block block, Vector3 direction)
        {
            float sliceAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            Transform blockTransform = block.transform;
            Vector3 blockPosition = blockTransform.position;
            BlockSpriteConfig.BlockSprites blockSprites = block.blockSprites;

            CreateJuiceParticle(blockPosition, blockSprites.juiceColor);
            CreateBlot(blockPosition, blockSprites.blotSprite);
            float leftBlockOrthogonalAngle = sliceAngle + 90f;
            float rightBlockOrthogonalAngle = sliceAngle - 90f;
            CreateHalfBlock(block, blockSprites.blockLeftHalf, leftBlockOrthogonalAngle);
            CreateHalfBlock(block, blockSprites.blockRightHalf, rightBlockOrthogonalAngle);

            Destroy(block.gameObject);
        }

        public Juice CreateJuiceParticle(Vector3 parentBlockPosition, Color juiceColor)
        {
            var juice = Instantiate(juicePrefab, parentBlockPosition, Quaternion.identity);
            juice.Initialize(juiceColor);
            return juice;
        }

        public Blot CreateBlot(Vector3 parentBlockPosition, Sprite blotSprite)
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
            halfBlock.blockSprites.blockSprite = halfBlockSprite;
            halfBlock.blockAnimation.disableScaling = true;
            halfBlock.blockMovement.InitialSpeed = halfBlockInitialSpeed;
            halfBlock.blockMovement.InitialAngle = initialAngle;
            halfBlock.lifeTime = halfBlockLifeTime;
            halfBlock.Initialize();
            return halfBlock;
        }
        
        public Rect GetDestroyAreaRect()
        {
            Rect destroyArea = spawnersController.cameraController.GetCameraRect();
            destroyArea.height *= spawnersController.managerConfig.destroyAreaScale;
            destroyArea.width *= spawnersController.managerConfig.destroyAreaScale;
            destroyArea.center = Vector2.zero;
            return destroyArea;
        }
        
        private void Update()
        {
            DestroyScreenOutBlocks();
        }

        private void DestroyScreenOutBlocks()
        {
            for (int i = 0; i < spawnersController.spawnedBlocks.blockList.Count; i++)
            {
                Block block = spawnersController.spawnedBlocks.blockList[i];
                if (IsOutOfScreen(block.transform))
                {
                    Destroy(block.gameObject);
                    spawnersController.spawnedBlocks.blockList.RemoveAt(i);
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