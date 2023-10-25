using App.Scripts.Game.Animations;
using App.Scripts.Game.Blades;
using App.Scripts.Game.InputSystem;
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
        [SerializeField] private SpawnersManager spawnersManager;
        [SerializeField] private BladeMovement bladeMovement;
        [SerializeField] private LiveBar liveBar;
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private Popup popup;
        [SerializeField] private Image loadImage;
        [SerializeField] private ButtonManager pauseButton;
        [SerializeField] private float loadSceneAnimationDuration = 1f;
        
        private readonly UIAnimation _uiAnimation = new();
        private void Awake()
        {
            pauseButton.ButtonAction = popup.PauseGame;
            Time.timeScale = 1f;
            IInput inputController = GetInputController();
            bladeMovement.Initialize(inputController);
            liveBar.Initialize();
            scoreBar.Initialize();
            spawnersManager.Initialize();
            popup.Initialize();
            AnimateSceneLoad(loadImage, loadSceneAnimationDuration);
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