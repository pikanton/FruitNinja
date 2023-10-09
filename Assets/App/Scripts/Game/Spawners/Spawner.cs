using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Spawners
{
    public class Spawner : MonoBehaviour
    {
        public SpawnersManager spawnersManager;
        public Color color = Color.black;
        [Range(-0.6f, 0.6f)]
        public float relativePositionX;
        [Range(-0.6f, 0.6f)]
        public float relativePositionY;
        [Range(0f, 1.2f)]
        public float relativeSpawnerLength = 0.5f;
        [Range(-180f, 180f)]
        public float spawnerAngle = 0f;
        [Range(0f, 180f)]
        public float firstAngle = 30f;
        [Range(0f, 180f)]
        public float secondAngle = 60f;
        public float probability = 1f;

        private float _angleDrawLength = 2f;
        private float _relativeFirstAngle;
        private float _relativeSecondAngle;
        private float _spawnerLength;

        public void Initialize()
        {
            UpdateSpawner();
        }
        
        public float GetRandomAngle()
        {
            float angle;
            if (_relativeFirstAngle > _relativeSecondAngle)
                angle = Random.Range(_relativeFirstAngle, _relativeSecondAngle);
            else
                angle = Random.Range(_relativeSecondAngle, _relativeFirstAngle);
            return angle;
        }

        public Vector3 GetRandomPosition()
        {
            float halfSpawnerLength = _spawnerLength / 2f;
            float randomLength = Random.Range(-halfSpawnerLength, halfSpawnerLength);
            
            Vector3 randomPosition = GetNormalizedVectorByAngle(spawnerAngle) * randomLength;
            Vector3 spawnerPosition = transform.position;
            randomPosition += spawnerPosition;
            
            return randomPosition;
        }
        
        private void OnValidate()
        {
            UpdateSpawner();
        }

        private void UpdateSpawner()
        {
            _relativeFirstAngle = firstAngle + spawnerAngle;
            _relativeSecondAngle = secondAngle + spawnerAngle;

            Rect screenSize = spawnersManager.cameraManager.GetCameraRect();
            SetPositionRelativeToScreen(screenSize.width, screenSize.height);
            SetLengthRelativeToScreen(screenSize.width, screenSize.height);
        }

        private void SetPositionRelativeToScreen(float screenWidth, float screenHeight)
        {
            Vector3 newPosition = new Vector3();
            newPosition.x = relativePositionX * screenWidth;
            newPosition.y = relativePositionY * screenHeight;
            transform.position = newPosition; 
        }
        
        private void SetLengthRelativeToScreen(float screenWidth, float screenHeight)
        {
            Vector3 length = GetNormalizedVectorByAngle(spawnerAngle) * relativeSpawnerLength;

            float xs = Mathf.Pow(length.x * screenWidth, 2f);
            float ys = Mathf.Pow(length.y * screenHeight, 2f);

            _spawnerLength = Mathf.Sqrt(xs + ys);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Vector3 spawnerPosition = transform.position;

            UpdateSpawner();
            
            DrawSpawnerLength(spawnerPosition, spawnerAngle);
            DrawAngleLine(spawnerPosition, _relativeFirstAngle);
            DrawAngleLine(spawnerPosition, _relativeSecondAngle);
        }

        private void DrawSpawnerLength(Vector3 startPosition, float angle)
        {
            float halfSpawnerLength = _spawnerLength / 2f;
            Vector3 endPoint = GetNormalizedVectorByAngle(angle) * halfSpawnerLength + startPosition;
            Gizmos.DrawLine(startPosition, endPoint);
            
            endPoint = GetNormalizedVectorByAngle(angle) * -halfSpawnerLength + startPosition;
            Gizmos.DrawLine(startPosition, endPoint);
        }
        
        private void DrawAngleLine(Vector3 startPosition, float angle)
        {
            Vector3 endPoint = GetNormalizedVectorByAngle(angle) * _angleDrawLength + startPosition;
            Gizmos.DrawLine(startPosition, endPoint);
        }
        
        private Vector3 GetNormalizedVectorByAngle(float angleInDegrees)
        {
            float angleInRadians = angleInDegrees  * Mathf.Deg2Rad;
            Vector3 vector = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f);
            return vector;
        }
    }
}