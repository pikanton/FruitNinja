using App.Scripts.Game.Controllers;
using UnityEngine;

namespace App.Scripts.Game.EditorGizmos
{
    public class DestroyAreaGizmos : MonoBehaviour
    {
        public BlockDestroyController blockDestroyController;
        private void OnDrawGizmos()
        {
            DrawDestroyArea();
        }

        private void DrawDestroyArea()
        {
            Gizmos.color = Color.red;
            
            Vector3 centerOfScreen = Vector3.zero;
            Rect destroyArea = blockDestroyController.GetDestroyAreaRect();
            Vector3 sizeOfDestroyArea = new Vector3(destroyArea.width, destroyArea.height, 0f);
            
            Gizmos.DrawWireCube(centerOfScreen, sizeOfDestroyArea);
        }
    }
}