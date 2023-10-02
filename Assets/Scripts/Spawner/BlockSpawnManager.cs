using UnityEngine;

public class BlockSpawnManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public float bottomLeftBorder = 0.2f; // От 0 до 1
    public float bottomRightBorder = 0.8f;
    public float lateralLowerBorder = 0.2f;
    public float lateralHigherBorder = 0.5f;
    public float spawnRateSeconds = 1f;
    public float bottomSpawnPrecent = 0.8f;

    private float nextSpawnTime;
    private float screenHeight;
    private float screenWidth;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnRateSeconds;
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
    }
    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            float x, y, z = 0;
            float precent = Random.value;
            if (precent <= bottomSpawnPrecent)
            {
                float leftBorder = 0 - screenWidth / 2f + bottomLeftBorder * screenWidth;
                float rightBorder = 0 - screenWidth / 2f + bottomRightBorder * screenWidth;
                x = Random.Range(leftBorder, rightBorder);
                y = 0 - screenHeight / 2f;
            }
            else
            {
                float bottomBorder = 0 - screenHeight / 2f + lateralLowerBorder * screenHeight;
                float topBorder = 0 - screenHeight / 2f + lateralHigherBorder * screenHeight;
                x = 0 - screenWidth / 2f;
                y = Random.Range(bottomBorder, topBorder);
            }
            Vector3 spawnPosition = new Vector3(x, y, z);
            GameObject newBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

            nextSpawnTime = Time.time + spawnRateSeconds;
        }
    }
}
