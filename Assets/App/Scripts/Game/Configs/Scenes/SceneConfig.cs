using UnityEngine;

namespace App.Scripts.Game.Configs.Scenes
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/Scenes/SceneConfig")]
    public class SceneConfig : ScriptableObject
    {
        public float loadSceneAnimationDuration = 1f;
        public int targetFrameRate = 120;
        public int vSyncCount = 0;
        public float startBlocksTimeScale = 1f;
        public float startTimeScale = 1f;
        
        public string gameSceneName = "Game";
        public string menuSceneName = "Menu";
    }
}