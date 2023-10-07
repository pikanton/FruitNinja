using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject blockPrefab;

    [Range(0f, 1f)] public float bottomSpawnPercent = 0.8f;
    [Range(0f, 1f)] public float bottomLeftBorder = 0.2f;
    [Range(0f, 1f)] public float bottomRightBorder = 0.8f;
    [Range(0, 90f)] public float bottomFirstAngle = 60f;
    [Range(0, 90f)] public float bottomSecondAngle = 90f;

    [Range(0f, 1f)] public float lateralLowerBorder = 0.2f;
    [Range(0f, 1f)] public float lateralHigherBorder = 0.4f;
    [Range(-90f, 90f)] public float lateralFirstAngle = 20f;
    [Range(-90f, 90f)] public float lateralSecondAngle = 40f;

    public int minBlockCountInStack = 2;
    public int maxBlockCountInStack = 5;

    public float difficultyIncreaseTime = 10f;
    public float spawnReductionTimeFactor = 0.1f;
    public float stackSpawnTime = 4f;
    public float blockSpawnTime = 0.4f;
    public float startSpawnDelay = 1f;
    
    private const float SpawnAreaScale = 1.1f;
    
    private float _screenHeight;
    private float _screenWidth;
    
    private int _currentBlockCountInStack;
    private int _spawnedBlockCount;

    private float _nextDifficultyIncreaseTime;
    private float _currentReductionTimeFactor;
    private float _spawnRateReductionFactor;
    private float _nextStackSpawnTime;
    private float _nextBlockSpawnTime;


    private void Start()
    {
        var mainCamera = Camera.main;
        if (mainCamera is null)
            throw new Exception("Cannot find camera.");
        _screenHeight = mainCamera.orthographicSize * 2f;
        _screenWidth = _screenHeight * mainCamera.aspect;

        _currentBlockCountInStack = minBlockCountInStack;
        _spawnedBlockCount = 0;

        _currentReductionTimeFactor = 1f;
        _nextStackSpawnTime = Time.time + startSpawnDelay;
        _nextDifficultyIncreaseTime = Time.time + startSpawnDelay + difficultyIncreaseTime;
    }

    private void Update()
    {
        MakeMoreDifficult();
        SpawnStacks();
    }

    private GameObject SpawnBlock()
    {
        float x, y;
        float angle;
        float percent = Random.value;
        if (percent <= bottomSpawnPercent)
        {
            float leftBorder = -_screenWidth / 2f + bottomLeftBorder * _screenWidth;
            float rightBorder = -_screenWidth / 2f + bottomRightBorder * _screenWidth;
            x = Random.Range(leftBorder, rightBorder);
            y = 0 - _screenHeight * SpawnAreaScale / 2f;
            if (x < 0)
                angle = Random.Range(bottomFirstAngle, bottomSecondAngle);
            else
                angle = Random.Range(180f - bottomFirstAngle, 180f - bottomSecondAngle);
        }
        else
        {
            float bottomBorder = -_screenHeight / 2f + lateralLowerBorder * _screenHeight;
            float topBorder = -_screenHeight / 2f + lateralHigherBorder * _screenHeight;
            y = Random.Range(bottomBorder, topBorder);
            if (percent - bottomSpawnPercent <= (1f - bottomSpawnPercent) / 2f)
            {
                x = 0 - _screenWidth * SpawnAreaScale / 2f;
                angle = Random.Range(lateralFirstAngle, lateralSecondAngle);
            }
            else
            {
                x = 0 + _screenWidth * SpawnAreaScale / 2f;
                angle = Random.Range(180f - lateralFirstAngle, 180f - lateralSecondAngle);
            }
        }

        Vector2 spawnPosition = new Vector3(x, y);
        GameObject newBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        newBlock.GetComponent<BlockMovement>().SetLaunchAngle(angle);

        return newBlock;
    }

    private void SpawnStacks()
    {
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
    
    private void MakeMoreDifficult()
    {
        if (_currentBlockCountInStack < maxBlockCountInStack && _nextDifficultyIncreaseTime < Time.time)
        {
            _currentBlockCountInStack++;
            _currentReductionTimeFactor -= spawnReductionTimeFactor;
            _nextDifficultyIncreaseTime = Time.time + difficultyIncreaseTime;
        }
    }
}