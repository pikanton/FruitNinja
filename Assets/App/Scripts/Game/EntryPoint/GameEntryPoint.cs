using App.Scripts.Game.Animations;
using App.Scripts.Game.Blades;
using App.Scripts.Game.Configs.Scenes;
using App.Scripts.Game.InputSystem;
using App.Scripts.Game.SceneManagers;
using App.Scripts.Game.Spawners;
using App.Scripts.Game.UISystem.Lives;
using App.Scripts.Game.UISystem.Popups;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;
using UnityEngine.UI;
using AndroidInput = App.Scripts.Game.InputSystem.AndroidInput;

namespace App.Scripts.Game.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneConfig sceneConfig;
        [SerializeField] private SpawnersManager spawnersManager;
        [SerializeField] private BladeMovement bladeMovement;
        [SerializeField] private LiveBar liveBar;
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private Popup popup;
        [SerializeField] private Image loadImage;
        [SerializeField] private ButtonManager pauseButton;
        
        private readonly UIAnimation _uiAnimation = new();
        private void Awake()
        {
            SetProperties();
            SetComponents();
            AnimateSceneLoad(loadImage, sceneConfig.loadSceneAnimationDuration);
        }

        private void SetProperties()
        {
            Application.targetFrameRate = sceneConfig.targetFrameRate;
            QualitySettings.vSyncCount = sceneConfig.vSyncCount;
            SceneProperties.BlocksTimeScale = sceneConfig.startBlocksTimeScale;
            Time.timeScale = sceneConfig.startTimeScale;
        }
        
        private void SetComponents()
        {
            pauseButton.ButtonAction = popup.PauseGame;
            IInput inputController = GetInputController();
            bladeMovement.Initialize(inputController);
            liveBar.Initialize();
            scoreBar.Initialize();
            spawnersManager.Initialize();
            popup.Initialize();
        }
        
        private IInput GetInputController()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return new AndroidInput();
            }
            else
            {
                return new WindowsInput();
            }
        }
        
        private void AnimateSceneLoad(Image fadeImage, float animationDuration)
        {
            Color initialColor = fadeImage.color;
            fadeImage.enabled = true;
            fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
            StartCoroutine(_uiAnimation.FadeAnimation(fadeImage, 0f, animationDuration));
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => fadeImage.enabled = false, animationDuration));
        }
    }
}