using UnityEngine;

namespace App.Scripts.Game.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public UnityEngine.Camera camera;
        
        public Rect GetCameraRect()
        {
            float cameraHeight = camera.orthographicSize * 2f;
            float cameraWidth = cameraHeight * camera.aspect;
    
            float halfCameraWidth = cameraWidth * 0.5f;
            float halfCameraHeight = cameraHeight * 0.5f;
    
            return new Rect(-halfCameraWidth, -halfCameraHeight, cameraWidth, cameraHeight);
        }
    }
}