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
        
        private readonly GameSaver _gameSaver = new();
        private readonly UIAnimation _uiAnimation = new();

        public void Awake()
        {
            Time.timeScale = 1f;
            int score = _gameSaver.GetHighScore();
            scoreTextMeshPro.text = score.ToString();
            AnimateSceneLoad(loadImage, loadSceneAnimationDuration);
        }

        public void LoadGameScene()
        {
            AnimateSceneQuit();
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => SceneManager.LoadScene(gameSceneName), loadSceneAnimationDuration));
        }

        public void Exit()
        {
            AnimateSceneQuit();
            StartCoroutine(_uiAnimation.DoActionAfterDelay(Quit, loadSceneAnimationDuration));
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