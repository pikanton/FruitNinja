using App.Scripts.Game.Spawners;
using UnityEngine;

namespace App.Scripts.Game.EditorGizmos
{
    public class SpawnerGizmos : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;

        [SerializeField] private Color color = Color.black;
        [SerializeField] private float angleDrawLength = 2f;

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Vector3 spawnerPosition = spawner.GetPositionRelativeToScreen();

            DrawSpawnerLength(spawnerPosition, spawner.spawnerAngle);
            DrawAngleLine(spawnerPosition, spawner.firstAngle + spawner.spawnerAngle);
            DrawAngleLine(spawnerPosition, spawner.secondAngle + spawner.spawnerAngle);
        }

        private void DrawSpawnerLength(Vector3 startPosition, float angle)
        {
            float halfSpawnerLength = spawner.GetLengthRelativeToScreen() / 2f;
            Vector3 endPoint = spawner.GetNormalizedVectorByAngle(angle) * halfSpawnerLength + startPosition;
            Gizmos.DrawLine(startPosition, endPoint);

            endPoint = spawner.GetNormalizedVectorByAngle(angle) * -halfSpawnerLength + startPosition;
            Gizmos.DrawLine(startPosition, endPoint);
        }
        
        private void DrawAngleLine(Vector3 startPosition, float angle)
        {
            Vector3 endPoint = spawner.GetNormalizedVectorByAngle(angle) * angleDrawLength + startPosition;
            Gizmos.DrawLine(startPosition, endPoint);
        }
    }
}