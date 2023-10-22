using App.Scripts.Game.Animations;
using App.Scripts.Game.Saves;
using TMPro;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class ScoreBar : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreTextMeshPro;
        [SerializeField] public TextMeshProUGUI recordTextMeshPro;
        [SerializeField] private float scoreAnimationDuration = 0.5f;
        [SerializeField] private string prefix = "Лучший: ";

        private GameSaver _gameSaver = new GameSaver();

        private UIAnimation _uiAnimation = new();

        public int CurrentScore { private set; get; }
        public int HighScore { private set; get; }

        public void Initialize()
        {
            CurrentScore = 0;
            HighScore = _gameSaver.GetHighScore();
            scoreTextMeshPro.text = CurrentScore.ToString();
            recordTextMeshPro.text = GetHighScoreString();
        }

        public void AddScore(int amount)
        {
            StartCoroutine(_uiAnimation.AnimateValueChange(CurrentScore, amount, scoreAnimationDuration,
                scoreTextMeshPro));
            
            CurrentScore += amount;
            
            if (HighScore < CurrentScore)
            {
                StartCoroutine(_uiAnimation.AnimateValueChange(HighScore, CurrentScore - HighScore,
                    scoreAnimationDuration, recordTextMeshPro, prefix));
                HighScore = CurrentScore;
            }
        }

        public int GetHighScore()
        {
            return HighScore;
        }

        public string GetHighScoreString()
        {
            return $"{prefix}{HighScore.ToString()}";
        }
    }
}