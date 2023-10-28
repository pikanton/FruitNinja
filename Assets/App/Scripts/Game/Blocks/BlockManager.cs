using App.Scripts.Game.SceneManagers;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Game.Blocks
{
    public class BlockManager : MonoBehaviour
    {

        private void Update()
        {
            if (BlockCounter.FreezerIsDestroyed && 
                BlockCounter.FreezerCount > 0 && SceneProperties.BlocksTimeScale >= 1f)
            {
                BlockCounter.FreezerIsDestroyed = false;
                BlockCounter.FreezerCount = 0;
            }
        }
    }
}