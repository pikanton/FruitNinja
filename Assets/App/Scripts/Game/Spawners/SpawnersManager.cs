using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
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

        private List<GameObject> _spawnedBlocks;
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
            _spawnedBlocks = new List<GameObject>();
            SetDestroyArea();
            SetBlockCounts();
            SetTimers();
        }

        private void SetDestroyArea()
        {
            float destroyAreaScale = managerConfig.destroyAreaScale;
            Rect cameraSize = cameraManager.GetCameraRect();
            _destroyAreaWidth = cameraSize.width * destroyAreaScale;
            _destroyAreaHeight = cameraSize.height * destroyAreaScale;
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
        
        private void Update()
        {
            DestroyBlocks();
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
            _spawnedBlocks.Add(newBlock);
        }
        
        private void DestroyBlocks()
        {
            for (int i = 0; i < _spawnedBlocks.Count; i++)
            {
                GameObject block = _spawnedBlocks[i];
                if (IsOutOfScreen(block.transform))
                {
                    Destroy(block);
                    _spawnedBlocks.RemoveAt(i);
                }
            }
        }

        private bool IsOutOfScreen(Transform blockTransform)
        {
            Vector3 position = blockTransform.position;

            float halfDestroyAreaWidth = _destroyAreaWidth / 2f;
            float halfDestroyAreaHeight = _destroyAreaHeight / 2f;

            return position.y < -halfDestroyAreaHeight ||
                   position.y > halfDestroyAreaHeight ||
                   position.x < -halfDestroyAreaWidth ||
                   position.x > halfDestroyAreaWidth;
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