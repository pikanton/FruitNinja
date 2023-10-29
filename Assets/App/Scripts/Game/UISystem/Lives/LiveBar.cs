using System.Collections.Generic;
using App.Scripts.Game.Animations;
using App.Scripts.Game.Configs.UI;
using App.Scripts.Game.Saves;
using App.Scripts.Game.UISystem.Popups;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Lives
{
    public class LiveBar : MonoBehaviour
    {
        [SerializeField] private List<Live> liveList;
        [SerializeField] private LivesConfig livesConfig;
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private Popup gamePopup;
        
        private readonly GameSaver _gameSaver = new();
        private readonly UIAnimation _uiAnimation = new();
        private bool _isCheckingLives = true;
        private int _currentLiveCount;
    
        public void Initialize()
        {
            _currentLiveCount = livesConfig.liveCount;
            for (int i = 0; i < liveList.Count; i++)
            {
                liveList[i].transform.localScale = i < _currentLiveCount ? Vector3.one : Vector3.zero;
            }
        }

        private void Update()
        {
            if (_currentLiveCount <= 0 && _isCheckingLives)
            {
                _isCheckingLives = false;
                _gameSaver.SaveHighScore(scoreBar.GetHighScore());
                gamePopup.StopGame();
            }
        }

        public void AddLive()
        {
            if (_currentLiveCount < liveList.Count)
            {
                StartCoroutine(_uiAnimation.ScaleAnimation(liveList[_currentLiveCount].transform,
                    Vector3.one, livesConfig.animationDuration));
                _currentLiveCount++;
            }
        }

        public void RemoveLive()
        {
            if (_currentLiveCount > 0)
            {
                _currentLiveCount--;
                StartCoroutine(_uiAnimation.ScaleAnimation(liveList[_currentLiveCount].transform,
                    Vector3.zero, livesConfig.animationDuration));
            }
        }

        private void OnValidate()
        {
            Initialize();
        }
    }
}