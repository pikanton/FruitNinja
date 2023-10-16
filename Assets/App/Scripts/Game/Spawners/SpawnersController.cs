using UnityEngine;
using System.Collections.Generic;
using App.Scripts.Game.Blocks;
using Random = UnityEngine.Random;
using App.Scripts.Game.Configs;
using App.Scripts.Game.Controllers;

namespace App.Scripts.Game.Spawners
{
    public class SpawnersController : MonoBehaviour
    {
        [SerializeField] private Block blockPrefab;
        [SerializeField] private List<Spawner> spawners;
        [SerializeField] private BlockSpriteConfig blockSprite;
        [SerializeField] public CameraController cameraController;
        [SerializeField] public SpawnersManagerConfig managerConfig;
        [SerializeField] public BlockList spawnedBlocks;
        
        private int _currentBlockCountInStack;
        private int _spawnedBlockCount;
        private float _nextDifficultyIncreaseTime;
        private float _currentReductionTimeFactor = 1f;
        private float _spawnRateReductionFactor;
        private float _nextStackSpawnTime;
        private float _nextBlockSpawnTime;

        
        public void Initialize()
        {
            SetBlockCounts();
            SetTimers();
        }
        
        private void SetTimers()
        {
            float startSpawnDelay = managerConfig.startSpawnDelay;
            float difficultyIncreaseTime = managerConfig.difficultyIncreaseTime;
            _nextStackSpawnTime = Time.time + startSpawnDelay;
            _nextDifficultyIncreaseTime = Time.time + startSpawnDelay + difficultyIncreaseTime;
        }

        private void SetBlockCounts()
        {
            int minBlockCountInStack = managerConfig.minBlockCountInStack;
            _currentBlockCountInStack = minBlockCountInStack;
        }
        
        private void Update()
        {
            MakeMoreDifficult();
            SpawnStacks();
        }

        private void SpawnStacks()
        {
            float stackSpawnTime = managerConfig.stackSpawnTime;
            float blockSpawnTime = managerConfig.blockSpawnTime;
            
            if (Time.time < _nextStackSpawnTime)
                return;
        
            if (_spawnedBlockCount >= _currentBlockCountInStack)
            {
                _nextStackSpawnTime = Time.time + stackSpawnTime * _currentReductionTimeFactor;
                _spawnedBlockCount = 0;
                return;
            }

            if (Time.time < _nextBlockSpawnTime)
                return;
        
            SpawnBlock();
            _spawnedBlockCount++;
        
            _nextBlockSpawnTime = Time.time + blockSpawnTime * _currentReductionTimeFactor;
        }

        private void SpawnBlock()
        {
            Spawner randomSpawner = GetRandomSpawner();
            Vector3 spawnPosition = randomSpawner.GetRandomPosition();
            float spawnAngle = randomSpawner.GetRandomAngle();
            
            var newBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity, spawnedBlocks.gameObject.transform);
            var containerBlock = blockSprite.GetRandomBlockView();
            newBlock.blockSprites = containerBlock;
            
            newBlock.blockMovement.InitialAngle = spawnAngle;
            newBlock.blockMovement.InitialSpeed = managerConfig.blockInitialSpeed;
            
            newBlock.Initialize();
            spawnedBlocks.blockList.Add(newBlock);
        }
        
        private void MakeMoreDifficult()
        {
            int maxBlockCountInStack = managerConfig.maxBlockCountInStack;
            float spawnReductionTimeFactor = managerConfig.spawnReductionTimeFactor;
            float difficultyIncreaseTime = managerConfig.difficultyIncreaseTime;
            
            if (_currentBlockCountInStack < maxBlockCountInStack && _nextDifficultyIncreaseTime < Time.time)
            {
                _currentBlockCountInStack++;
                _currentReductionTimeFactor -= spawnReductionTimeFactor;
                _nextDifficultyIncreaseTime = Time.time + difficultyIncreaseTime;
            }
        }
        
        private Spawner GetRandomSpawner()
        {
            float sumOfProbabilities = 0f;
            for (int i = 0; i < spawners.Count; i++)
            {
                sumOfProbabilities += spawners[i].probability;
            }

            float randomProbabilityOfSpawner = Random.Range(0f, sumOfProbabilities);
            
            for (int i = 0; i < spawners.Count; i++)
            {
                randomProbabilityOfSpawner -= spawners[i].probability;
                if (randomProbabilityOfSpawner <= 0)
                    return spawners[i];
            }

            return null;
        }
        
    }
}