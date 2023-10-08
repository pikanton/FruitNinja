using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using App.Scripts.Game.Block;
using App.Scripts.Game.Camera;
using App.Scripts.Game.Configs;

namespace App.Scripts.Game.Spawners
{
    public class SpawnersManager : MonoBehaviour
    {
        public CameraManager cameraManager;
        public GameObject blockPrefab;
        public List<Spawner> spawners;

        public SpawnersManagerConfig managerConfig;
        
        private int _currentBlockCountInStack;
        private int _spawnedBlockCount;
        private float _nextDifficultyIncreaseTime;
        private float _currentReductionTimeFactor;
        private float _spawnRateReductionFactor;
        private float _nextStackSpawnTime;
        private float _nextBlockSpawnTime;
        private float _destroyAreaWidth;
        private float _destroyAreaHeight;
        
        public void Initialize()
        {
            SetDestroyArea();
            SetBlockCounts();
            SetTimers();
        }

        private void SetDestroyArea()
        {
            float destroyAreaScale = managerConfig.destroyAreaScale;
            _destroyAreaWidth = cameraManager.GetCameraWidth() * destroyAreaScale;
            _destroyAreaHeight = cameraManager.GetCameraHeight() * destroyAreaScale;
        }
        
        private void SetTimers()
        {
            float startSpawnDelay = managerConfig.startSpawnDelay;
            float difficultyIncreaseTime = managerConfig.difficultyIncreaseTime;
            _currentReductionTimeFactor = 1f;
            _nextStackSpawnTime = Time.time + startSpawnDelay;
            _nextDifficultyIncreaseTime = Time.time + startSpawnDelay + difficultyIncreaseTime;
        }

        private void SetBlockCounts()
        {
            int minBlockCountInStack = managerConfig.minBlockCountInStack;
            _currentBlockCountInStack = minBlockCountInStack;
            _spawnedBlockCount = 0;
        }
        
        private void OnDrawGizmos()
        {
            SetDestroyArea();
            DrawDestroyArea();
        }

        private void DrawDestroyArea()
        {
            Gizmos.color = Color.red;
            
            Vector3 centerOfScreen = Vector3.zero;
            Vector3 sizeOfDestroyArea = new Vector3(_destroyAreaWidth, _destroyAreaHeight, 0f);
            
            Gizmos.DrawWireCube(centerOfScreen, sizeOfDestroyArea);
        }
        
        private void FixedUpdate()
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
            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, spawnAngle);
            GameObject newBlock = Instantiate(blockPrefab, spawnPosition, spawnRotation);
            newBlock.GetComponent<BlockMovement>().SetDestroyArea(_destroyAreaWidth, _destroyAreaHeight);
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