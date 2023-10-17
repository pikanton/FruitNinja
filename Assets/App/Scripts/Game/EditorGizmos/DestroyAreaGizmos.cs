using App.Scripts.Game.SceneManagers;
using UnityEngine;

namespace App.Scripts.Game.EditorGizmos
{
    public class DestroyAreaGizmos : MonoBehaviour
    {
        [SerializeField] private BlockDestroyManager blockDestroyManager;
        private void OnDrawGizmos()
        {
            DrawDestroyArea();
        }

        private void DrawDestroyArea()
        {
            Gizmos.color = Color.red;
            
            Vector3 centerOfScreen = Vector3.zero;
            Rect destroyArea = blockDestroyManager.GetDestroyAreaRect();
            Vector3 sizeOfDestroyArea = new Vector3(destroyArea.width, destroyArea.height, 0f);
            
            Gizmos.DrawWireCube(centerOfScreen, sizeOfDestroyArea);
        }
    }
}