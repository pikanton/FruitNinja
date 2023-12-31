﻿using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnersManager spawnersManager;
        [Range(-0.6f, 0.6f)]
        [SerializeField] private float relativePositionX;
        [Range(-0.6f, 0.6f)]
        [SerializeField] private float relativePositionY;
        [Range(0f, 1.2f)]
        [SerializeField] private float relativeSpawnerLength = 0.5f;
        [Range(-180f, 180f)]
        [SerializeField] public float spawnerAngle;
        [Range(0f, 180f)]
        [SerializeField] public float firstAngle = 30f;
        [Range(0f, 180f)]
        [SerializeField] public float secondAngle = 60f;
        [SerializeField] public float probability = 1f;

        public float GetRandomAngle()
        {
            float relativeFirstAngle = firstAngle + spawnerAngle;
            float relativeSecondAngle = secondAngle + spawnerAngle;
            float angle;
            if (relativeFirstAngle > relativeSecondAngle)
                angle = Random.Range(relativeFirstAngle, relativeSecondAngle);
            else
                angle = Random.Range(relativeSecondAngle, relativeFirstAngle);
            return angle;
        }

        public Vector3 GetRandomPosition()
        {
            float halfSpawnerLength = GetLengthRelativeToScreen() / 2f;
            float randomLength = Random.Range(-halfSpawnerLength, halfSpawnerLength);
            
            Vector3 randomPosition = GetNormalizedVectorByAngle(spawnerAngle) * randomLength;
            Vector3 spawnerPosition = GetPositionRelativeToScreen();
            randomPosition += spawnerPosition;
            
            return randomPosition;
        }
        
        public Vector3 GetPositionRelativeToScreen()
        {
            Rect screenSize = spawnersManager.cameraManager.GetCameraRect();
            Vector3 newPosition = new Vector3
            {
                x = relativePositionX * screenSize.width,
                y = relativePositionY * screenSize.height
            };
            return newPosition; 
        }
        
        public float GetLengthRelativeToScreen()
        {
            Rect screenSize = spawnersManager.cameraManager.GetCameraRect();
            Vector3 length = GetNormalizedVectorByAngle(spawnerAngle) * relativeSpawnerLength;

            float squareOfNormalizedX = Mathf.Pow(length.x * screenSize.width, 2f);
            float squareOfNormalizedY = Mathf.Pow(length.y * screenSize.height, 2f);

            return Mathf.Sqrt(squareOfNormalizedX + squareOfNormalizedY);
        }

        public Vector3 GetNormalizedVectorByAngle(float angleInDegrees)
        {
            float angleInRadians = angleInDegrees  * Mathf.Deg2Rad;
            Vector3 vector = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            return vector;
        }
    }
}