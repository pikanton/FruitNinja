using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawnManager : MonoBehaviour
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

    public float spawnRateSeconds = 1f;

    private const float SpawnAreaScale = 1.1f;
    private float _nextSpawnTime;
    private float _screenHeight;
    private float _screenWidth;

    private void Start()
    {
        var mainCamera = Camera.main;
        if (mainCamera is null)
            throw new Exception("Cannot find camera.");
        _screenHeight = mainCamera.orthographicSize * 2f;
        _screenWidth = _screenHeight * mainCamera.aspect;
        
        _nextSpawnTime = Time.time + spawnRateSeconds;
    }

    private void Update()
    {
        if (Time.time < _nextSpawnTime)
            return;
        
        SpawnFruit();
        
        _nextSpawnTime = Time.time + spawnRateSeconds;
    }

    private GameObject SpawnFruit()
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
        GameObject newFruit = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        newFruit.GetComponent<FruitMovement>().SetLaunchAngle(angle);

        return newFruit;
    }
}