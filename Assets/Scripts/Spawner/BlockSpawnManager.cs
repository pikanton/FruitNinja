using UnityEngine;

public class BlockSpawnManager : MonoBehaviour
{
    public GameObject blockPrefab;

    [Range(0f, 1f)]
    public float bottomSpawnPrecent = 0.8f;
    [Range(0f, 1f)]
    public float bottomLeftBorder = 0.2f;
    [Range(0f, 1f)]
    public float bottomRightBorder = 0.8f;
    [Range(0, 90f)]
    public float bottomFirstAngle = 60f;
    [Range(0, 90f)]
    public float bottomSecondAngle = 90f;

    [Range(0f, 1f)]
    public float lateralLowerBorder = 0.2f;
    [Range(0f, 1f)]
    public float lateralHigherBorder = 0.4f;
    [Range(-90f, 90f)]
    public float lateralFirstAngle = 20f;
    [Range(-90f, 90f)]
    public float lateralSecondAngle = 40f;

    public float spawnRateSeconds = 1f;

    private float spawnAreaScale = 1.1f;
    private float nextSpawnTime;
    private float screenHeight;
    private float screenWidth;

    void Start()
    {
        nextSpawnTime = Time.time + spawnRateSeconds;
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
    }
    void Update()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
        if (Time.time >= nextSpawnTime)
        {
            float x, y;
            float angle;
            float precent = Random.value;
            if (precent <= bottomSpawnPrecent)
            {
                float leftBorder = -screenWidth / 2f + bottomLeftBorder * screenWidth;
                float rightBorder = -screenWidth / 2f + bottomRightBorder * screenWidth;
                x = Random.Range(leftBorder, rightBorder);
                y = 0 - screenHeight * spawnAreaScale / 2f;
                if (x < 0)
                    angle = Random.Range(bottomFirstAngle, bottomSecondAngle);
                else
                    angle = Random.Range(180f - bottomFirstAngle, 180f - bottomSecondAngle);
            }
            else
            {
                float bottomBorder = -screenHeight / 2f + lateralLowerBorder * screenHeight;
                float topBorder = -screenHeight / 2f + lateralHigherBorder * screenHeight;
                y = Random.Range(bottomBorder, topBorder);
                if (precent - bottomSpawnPrecent <= (1f - bottomSpawnPrecent) / 2f)
                {
                    x = 0 - screenWidth * spawnAreaScale / 2f;
                    angle = Random.Range(lateralFirstAngle, lateralSecondAngle);
                }
                else
                {
                    x = 0 + screenWidth * spawnAreaScale / 2f;
                    angle = Random.Range(180f - lateralFirstAngle, 180f - lateralSecondAngle);
                }
            }
            Vector2 spawnPosition = new Vector3(x, y);
            GameObject newFruit = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
            newFruit.GetComponent<FruitMovement>().SetLaunchAngle(angle);
            nextSpawnTime = Time.time + spawnRateSeconds;
        }
    }
}
