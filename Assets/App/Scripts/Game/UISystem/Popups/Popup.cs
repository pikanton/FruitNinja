using System;
using System.Collections;
using App.Scripts.Game.Animations;
using App.Scripts.Game.Blades;
using App.Scripts.Game.Blocks;
using App.Scripts.Game.Spawners;
using App.Scripts.Game.UISystem.Scores;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.Game.UISystem.Popups
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private SpawnersManager spawnersManager;
        [SerializeField] private BladeMovement bladeMovement;
        [SerializeField] private BlockList blocklist;
        [SerializeField] private ScoreBar scoreBar;
        
        [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
        [SerializeField] private TextMeshProUGUI recordTextMeshPro;
        [SerializeField] private TextMeshProUGUI infoTextMeshPro;
        [SerializeField] private TextMeshProUGUI actionButtonTextMeshPro;
        
        [SerializeField] private Button actionButton;
        [SerializeField] private Image panelImage;
        [SerializeField] private Transform containerTransform;

        [SerializeField] private float animationDuration = 0.5f;
        
        [SerializeField] private string continueInfoText = "ПАУЗА";
        [SerializeField] private string continueButtonText = "Продолжить";
        [SerializeField] private float continueButtonFontSize = 25f;
        [SerializeField] private string restartInfoText = "ВЫ ПРОИГРАЛИ!";
        [SerializeField] private string restartButtonText = "Рестарт";
        [SerializeField] private float restartButtonFontSize = 35f;

        [SerializeField] private float startPopupAlpha = 0f;
        [SerializeField] private float endPopupAlpha = 0.9f;
        
        private readonly UIAnimation _uiAnimation = new();
        
        public void PauseGame()
        {
            transform.localScale = Vector3.one;
            Time.timeScale = 0f;
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(Continue);
            ActivateManagers(false);
            actionButtonTextMeshPro.text = continueButtonText;
            actionButtonTextMeshPro.fontSize = continueButtonFontSize;
            infoTextMeshPro.text = continueInfoText;
            SetScoreText();
            StartCoroutine(_uiAnimation.FadeAnimation(panelImage, 0.9f, animationDuration));
            StartCoroutine(_uiAnimation.ScaleAnimation(containerTransform, Vector3.one, animationDuration));
        }

        public void StopGame()
        {
            transform.localScale = Vector3.one;
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(Restart);
            ActivateManagers(false);
            actionButtonTextMeshPro.text = restartButtonText;
            actionButtonTextMeshPro.fontSize = restartButtonFontSize;
            infoTextMeshPro.text = restartInfoText;
            SetScoreText();
            StartCoroutine(WaitForEmptyBlockListAndAnimate());
        }
                
        private void Restart()
        {
            StartCoroutine(DoActionAfterDelay(
                () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex),
                animationDuration));
            HidePopup();
        }

        private void Continue()
        {
            StartCoroutine(DoActionAfterDelay(
                () => ActivateManagers(true),
                animationDuration));
            StartCoroutine(DoActionAfterDelay(
                () => Time.timeScale = 1f,
                animationDuration));
            HidePopup();
        }

        private void ActivateManagers(bool active)
        {
            bladeMovement.gameObject.SetActive(active);
            spawnersManager.gameObject.SetActive(active);
        }
        
        private void SetScoreText()
        {
            scoreTextMeshPro.text = scoreBar.CurrentScore.ToString();
            recordTextMeshPro.text = scoreBar.GetHighScoreString();
        }
        
        private void HidePopup()
        {
            StartCoroutine(_uiAnimation.ScaleAnimation(containerTransform, Vector3.zero, animationDuration));
            StartCoroutine(_uiAnimation.FadeAnimation(panelImage, startPopupAlpha, animationDuration));
            StartCoroutine(DoActionAfterDelay(() => transform.localScale = Vector3.zero, animationDuration));
        }
        
        private void ShowPopup()
        {
            StartCoroutine(_uiAnimation.FadeAnimation(panelImage, endPopupAlpha, animationDuration));
            StartCoroutine(_uiAnimation.ScaleAnimation(containerTransform, Vector3.one, animationDuration));
        }
        
        private IEnumerator DoActionAfterDelay(Action action, float timeDelay)
        {
            float currentAnimationTime = 0;
            while (currentAnimationTime < timeDelay)
            {
                currentAnimationTime += Time.unscaledDeltaTime;
                yield return null;
            }
            action();
        }
        
        private IEnumerator WaitForEmptyBlockListAndAnimate()
        {
            while (blocklist.spawnedBlocks.Count > 0)
            {
                yield return null;
            }
            ShowPopup();
        }
    }
}