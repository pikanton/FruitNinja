using TMPro;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class ScoreBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
        [SerializeField] private TextMeshProUGUI recordTextMeshPro;
        [SerializeField] private float scoreAnimationDuration = 0.5f;
        [SerializeField] private string prefix = "Best: ";

        private int _highScore = 500;

        private Animations _animations = new();

        private int _currentScore;
        public void Initialize()
        {
            _currentScore = 0;
            scoreTextMeshPro.text = _currentScore.ToString();
            recordTextMeshPro.text = $"{prefix}{_highScore.ToString()}";
        }

        public void AddScore(int amount)
        {
            StartCoroutine(_animations.AnimateValueChange(_currentScore, amount, scoreAnimationDuration,
                scoreTextMeshPro));
            
            _currentScore += amount;
            
            if (_highScore < _currentScore)
            {
                StartCoroutine(_animations.AnimateValueChange(_highScore, _currentScore - _highScore, scoreAnimationDuration,
                    recordTextMeshPro, prefix));
                _highScore = _currentScore;
            }
        } 
    }
}