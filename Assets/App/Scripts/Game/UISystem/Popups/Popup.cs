using System;
using System.Collections;
using App.Scripts.Game.Animations;
using App.Scripts.Game.Blades;
using App.Scripts.Game.Blocks;
using App.Scripts.Game.Configs;
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
        
        [SerializeField] private PopupConfig popupConfig;

        

        private readonly UIAnimation _uiAnimation = new();

        public void Initialize()
        {
            containerTransform.localScale = Vector3.zero;
            transform.localScale = Vector3.zero;
            Color initialColor = panelImage.color;
            panelImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
            gameObject.SetActive(true);
        }
        
        public void LoadMenuScene()
        {
            StartCoroutine(_uiAnimation.ScaleAnimation(containerTransform, Vector3.zero, popupConfig.animationDuration));
            StartCoroutine(_uiAnimation.FadeAnimation(panelImage, 1f, popupConfig.animationDuration));
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => SceneManager.LoadScene(popupConfig.menuSceneName),
                popupConfig.animationDuration));
        }
        
        public void PauseGame()
        {
            Time.timeScale = 0f;
            ConfigurePopup(Continue);
            actionButtonTextMeshPro.text = popupConfig.continueButtonText;
            actionButtonTextMeshPro.fontSize = popupConfig.continueButtonFontSize;
            infoTextMeshPro.text = popupConfig.continueInfoText;
            StartCoroutine(_uiAnimation.FadeAnimation(panelImage, popupConfig.endPopupAlpha, popupConfig.animationDuration));
            StartCoroutine(_uiAnimation.ScaleAnimation(containerTransform, Vector3.one, popupConfig.animationDuration));
        }

        public void StopGame()
        {
            ConfigurePopup(Restart);
            actionButtonTextMeshPro.text = popupConfig.restartButtonText;
            actionButtonTextMeshPro.fontSize = popupConfig.restartButtonFontSize;
            infoTextMeshPro.text = popupConfig.restartInfoText;
            ActivateManagers(false);
            StartCoroutine(WaitForEmptyBlockListAndAnimate());
        }

        private void ConfigurePopup(Action buttonAction)
        {
            transform.localScale = Vector3.one;
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(new(buttonAction));
            scoreTextMeshPro.text = scoreBar.CurrentScore.ToString();
            recordTextMeshPro.text = scoreBar.GetHighScoreString();
        }
        
        private void Restart()
        {
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex),
                popupConfig.animationDuration));
            HidePopupAnimate();
        }

        private void Continue()
        {
            StartCoroutine(_uiAnimation.DoActionAfterDelay(
                () => Time.timeScale = 1f,
                popupConfig.animationDuration));
            HidePopupAnimate();
        }

        private void ActivateManagers(bool active)
        {
            bladeMovement.gameObject.SetActive(active);
            spawnersManager.gameObject.SetActive(active);
        }
        
        private void HidePopupAnimate()
        {
            StartCoroutine(_uiAnimation.ScaleAnimation(containerTransform, Vector3.zero, popupConfig.animationDuration));
            StartCoroutine(_uiAnimation.FadeAnimation(panelImage, popupConfig.startPopupAlpha, popupConfig.animationDuration));
            StartCoroutine(_uiAnimation.DoActionAfterDelay(() => transform.localScale = Vector3.zero, popupConfig.animationDuration));
        }
        
        private void ShowPopupAnimate()
        {
            StartCoroutine(_uiAnimation.FadeAnimation(panelImage, popupConfig.endPopupAlpha, popupConfig.animationDuration));
            StartCoroutine(_uiAnimation.ScaleAnimation(containerTransform, Vector3.one, popupConfig.animationDuration));
        }
        
        private IEnumerator WaitForEmptyBlockListAndAnimate()
        {
            while (blocklist.spawnedBlocks.Count > 0)
            {
                yield return null;
            }
            ShowPopupAnimate();
        }
    }
}