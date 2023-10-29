using App.Scripts.Game.Animations;
using App.Scripts.Game.Configs.Scenes;
using App.Scripts.Game.Saves;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.Menu
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneConfig sceneConfig;
        [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
        [SerializeField] private Image loadImage;
        [SerializeField] private ButtonManager startButton;
        [SerializeField] private ButtonManager exitButton;
        
        private readonly GameSaver _gameSaver = new();
        private readonly UIAnimation _uiAnimation = new();

        private void Awake()
        {
            Application.targetFrameRate = sceneConfig.targetFrameRate;
            QualitySettings.vSyncCount = sceneConfig.vSyncCount;
            Time.timeScale = sceneConfig.startTimeScale;

            startButton.ButtonAction = LoadGameScene;
            exitButton.ButtonAction = Exit;
            scoreTextMeshPro.text = _gameSaver.GetHighScore().ToString();
            
            AnimateSceneLoad(loadImage, sceneConfig.loadSceneAnimationDuration);
        }

        private void LoadGameScene()
        {
            AnimateSceneQuit();
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => SceneManager.LoadScene(sceneConfig.gameSceneName), sceneConfig.loadSceneAnimationDuration));
        }

        private void Exit()
        {
            AnimateSceneQuit();
            StartCoroutine(_uiAnimation.DoActionAfterDelay(Quit, sceneConfig.loadSceneAnimationDuration));
        }

        private void AnimateSceneLoad(Image fadeImage, float animationDuration)
        {
            fadeImage.enabled = true;
            Color initialColor = fadeImage.color;
            fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
            StartCoroutine(_uiAnimation.FadeAnimation(fadeImage, 0f, animationDuration));
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => fadeImage.enabled = false, animationDuration));
        }
        
        private void AnimateSceneQuit()
        {
            Color initialColor = loadImage.color;
            loadImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
            loadImage.enabled = true;
            StartCoroutine(_uiAnimation.FadeAnimation(loadImage, 1f, sceneConfig.loadSceneAnimationDuration));
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}