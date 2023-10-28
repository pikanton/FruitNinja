using System;
using App.Scripts.Game.Animations;
using App.Scripts.Game.Saves;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.Menu
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
        [SerializeField] private Image loadImage;
        [SerializeField] private float loadSceneAnimationDuration = 1f;
        [SerializeField] private string gameSceneName = "Game";
        [SerializeField] private ButtonManager startButton;
        [SerializeField] private ButtonManager exitButton;
        
        private readonly GameSaver _gameSaver = new();
        private readonly UIAnimation _uiAnimation = new();

        private void Awake()
        {
            Application.targetFrameRate = 120;
            QualitySettings.vSyncCount = 0;
            startButton.ButtonAction = LoadGameScene;
            exitButton.ButtonAction = Exit;
            Time.timeScale = 1f;
            int score = _gameSaver.GetHighScore();
            scoreTextMeshPro.text = score.ToString();
            AnimateSceneLoad(loadImage, loadSceneAnimationDuration);
        }

        private void LoadGameScene()
        {
            AnimateSceneQuit();
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => SceneManager.LoadScene(gameSceneName), loadSceneAnimationDuration));
        }

        private void Exit()
        {
            AnimateSceneQuit();
            StartCoroutine(_uiAnimation.DoActionAfterDelay(Quit, loadSceneAnimationDuration));
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
            StartCoroutine(_uiAnimation.FadeAnimation(loadImage, 1f, loadSceneAnimationDuration));
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