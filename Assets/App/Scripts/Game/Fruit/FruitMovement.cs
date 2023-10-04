using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    public float initialSpeed = 14f;
    public float gravity = 9.8f;

    private float _launchAngle;
    private Vector3 _initialVelocity;
    private Vector3 _currentPosition;

    private const float DestroyAreaScale = 1.2f;
    private float _screenHeight;
    private float _screenWidth;

    private void Start()
    {
        var mainCamera = Camera.main;
        _screenHeight = mainCamera.orthographicSize * 2f;
        _screenWidth = _screenHeight * mainCamera.aspect;

        _initialVelocity = GetInitialVelocity();

        _currentPosition = transform.position;
    }

    private void Update()
    {
        _currentPosition += _initialVelocity * Time.deltaTime;

        _initialVelocity.y -= gravity * Time.deltaTime;

        transform.position = _currentPosition;

        if (IsOutOfScreen())
            Destroy(gameObject);
    }

    private Vector3 GetInitialVelocity()
    {
        float radianAngle = Mathf.Deg2Rad * _launchAngle;
        float initialVelocityX = initialSpeed * Mathf.Cos(radianAngle);
        float initialVelocityY = initialSpeed * Mathf.Sin(radianAngle);
        return new Vector3(initialVelocityX, initialVelocityY, 0f);
    }

    private bool IsOutOfScreen()
    {
        Vector3 position = transform.position;
        return position.y < -_screenHeight * DestroyAreaScale / 2f ||
               position.x < -_screenWidth * DestroyAreaScale / 2f ||
               position.x > _screenWidth * DestroyAreaScale / 2f;
    }
    
    public void SetLaunchAngle(float angle)
    {
        _launchAngle = angle;
    }
}