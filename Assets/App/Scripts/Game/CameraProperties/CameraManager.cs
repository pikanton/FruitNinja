using UnityEngine;

namespace App.Scripts.Game.CameraProperties
{
    public class CameraManager : MonoBehaviour
    {
        public Camera camera;
        
        public Rect GetCameraRect()
        {
            float halfCameraHeight = camera.orthographicSize;
            float cameraHeight = halfCameraHeight * 2f;
            float cameraWidth = cameraHeight * camera.aspect;
            float halfCameraWidth = cameraWidth * 0.5f;
    
            return new Rect(-halfCameraWidth, -halfCameraHeight, cameraWidth, cameraHeight);
        }
    }
}