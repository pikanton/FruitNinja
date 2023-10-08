using UnityEngine;

namespace App.Scripts.Game.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public UnityEngine.Camera camera;
        
        public float GetCameraHeight()
        {
            return camera.orthographicSize * 2f;
        }
        
        public float GetCameraWidth()
        {
            return GetCameraHeight() * camera.aspect;
        }
    }
}