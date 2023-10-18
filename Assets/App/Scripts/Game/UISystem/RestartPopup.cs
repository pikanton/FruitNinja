using System.Collections;
using App.Scripts.Game.Blades;
using App.Scripts.Game.Blocks;
using App.Scripts.Game.Spawners;
using App.Scripts.Game.UISystem.Scores;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Game.UISystem
{
    public class RestartPopup : MonoBehaviour
    {
        [SerializeField] private SpawnersManager spawnersManager;
        [SerializeField] private BladeMovement bladeMovement;
        [SerializeField] private BlockList blocklist;
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
        [SerializeField] private TextMeshProUGUI recordTextMeshPro;

        [SerializeField] private float animationDuration = 0.5f;
        
        private Animations showAnimations = new Animations();
        public void StopGame()
        {
            scoreTextMeshPro.text = scoreBar.scoreTextMeshPro.text;
            recordTextMeshPro.text = scoreBar.recordTextMeshPro.text;
            bladeMovement.gameObject.SetActive(false);
            spawnersManager.gameObject.SetActive(false);
            ShowPopup();
        }

        private void ShowPopup()
        {
            StartCoroutine(WaitForEmptyBlockListAndAnimate());
        }

        private IEnumerator WaitForEmptyBlockListAndAnimate()
        {
            while (blocklist.spawnedBlocks.Count > 0)
            {
                yield return null;
            }
            StartCoroutine(showAnimations.ScaleAnimation(transform, Vector3.one, animationDuration));
        }
        
        public void Restart()
        {
            LoadScene();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}