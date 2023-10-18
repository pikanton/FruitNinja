using App.Scripts.Game.Blocks;
using App.Scripts.Game.Configs;
using App.Scripts.Game.Saves;
using App.Scripts.Game.UISystem;
using App.Scripts.Game.UISystem.Lives;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Game.SceneManagers
{
    public class BlockDestroyManager : MonoBehaviour
    {
        [SerializeField] private SpawnersManagerConfig managerConfig;
        [SerializeField] private CameraManager cameraManager;
        
        [SerializeField] private BlockList blockList;
        [SerializeField] private LiveBar liveBar;
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private RestartPopup restartPopup;
        
        private GameSaver _gameSaver = new GameSaver();

        public Rect GetDestroyAreaRect()
        {
            Rect destroyArea = cameraManager.GetCameraRect();
            destroyArea.height *= managerConfig.destroyAreaScale;
            destroyArea.width *= managerConfig.destroyAreaScale;
            destroyArea.center = Vector2.zero;
            return destroyArea;
        }
        
        private void Update()
        {
            DestroyScreenOutBlocks();
        }

        private void DestroyScreenOutBlocks()
        {
            for (int i = 0; i < blockList.spawnedBlocks.Count; i++)
            {
                Block block = blockList.spawnedBlocks[i];
                if (IsOutOfScreen(block.transform))
                {
                    Destroy(block.gameObject);
                    blockList.spawnedBlocks.RemoveAt(i);
                    liveBar.RemoveLive();
                }
            }
            if (liveBar.CurrentLiveCount <= 0)
            {
                _gameSaver.SaveHighScore(scoreBar.GetHighScore());
                restartPopup.StopGame();
            }
        }

        private bool IsOutOfScreen(Transform blockTransform)
        {
            Vector3 position = blockTransform.position;
            Rect destroyArea = GetDestroyAreaRect();
            
            return !destroyArea.Contains(position);
        }
        
    }
}