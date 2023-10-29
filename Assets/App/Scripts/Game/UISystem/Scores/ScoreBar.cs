using App.Scripts.Game.Animations;
using App.Scripts.Game.Configs.UI;
using App.Scripts.Game.Saves;
using TMPro;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class ScoreBar : MonoBehaviour
    {
        [SerializeField] private ScoreConfig scoreConfig;
        [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
        [SerializeField] private TextMeshProUGUI recordTextMeshPro;
 
        public int CurrentScore { private set; get; }

        private int _highScore;
        private readonly GameSaver _gameSaver = new();
        private readonly UIAnimation _uiAnimation = new();

        public void Initialize()
        {
            CurrentScore = 0;
            _highScore = _gameSaver.GetHighScore();
            scoreTextMeshPro.text = CurrentScore.ToString();
            recordTextMeshPro.text = GetHighScoreString();
        }

        public void AddScore(int amount)
        {
            StartCoroutine(_uiAnimation.AnimateValueChange(CurrentScore, amount, scoreConfig.scoreAnimationDuration,
                scoreTextMeshPro));
            
            CurrentScore += amount;
            
            if (_highScore < CurrentScore)
            {
                StartCoroutine(_uiAnimation.AnimateValueChange(_highScore, CurrentScore - _highScore,
                    scoreConfig.scoreAnimationDuration, recordTextMeshPro, scoreConfig.prefix));
                _highScore = CurrentScore;
            }
        }

        public int GetHighScore()
        {
            return _highScore;
        }

        public string GetHighScoreString()
        {
            return $"{scoreConfig.prefix}{_highScore.ToString()}";
        }
    }
}