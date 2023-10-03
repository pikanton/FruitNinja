using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    public float initialSpeed = 14f;
    public float gravity = 9.8f;

    private float launchAngle;
    private Vector3 initialVelocity;
    private Vector3 currentPosition;

    private float destroyAreaScale = 1.2f;
    private float screenHeight;
    private float screenWidth;

    private void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;

        float radianAngle = Mathf.Deg2Rad * launchAngle;
        float initialVelocityX = initialSpeed * Mathf.Cos(radianAngle);
        float initialVelocityY = initialSpeed * Mathf.Sin(radianAngle);

        initialVelocity = new Vector3(initialVelocityX, initialVelocityY, 0f);

        currentPosition = transform.position;
    }

    private void Update()
    {
        currentPosition += initialVelocity * Time.deltaTime;

        initialVelocity.y -= gravity * Time.deltaTime;

        transform.position = currentPosition;
        
        if (transform.position.y < -screenHeight * destroyAreaScale / 2f ||
                transform.position.x < -screenWidth * destroyAreaScale / 2f ||
                    transform.position.x > screenWidth * destroyAreaScale / 2f)
            Destroy(gameObject);
    }
    public void SetLaunchAngle(float angle)
    {
        launchAngle = angle;
    }
}
