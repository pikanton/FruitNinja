using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private ScoreLabel scoreLabel;
        [SerializeField] private MultiScoreLabel multiScoreLabel;
        [SerializeField] private float multiplyScoreDelay = 0.2f;
        [SerializeField] private int maxScoreMultiplayer = 5;
        
        private float _addScoreTime;
        private int _currentScoreMultiplayer = 2;

        public void AddScore(Vector3 blockPosition, int amount)
        {
            if (Time.time < _addScoreTime + multiplyScoreDelay)
            {
                scoreBar.AddScore(amount * _currentScoreMultiplayer);
                MultiScoreLabel newScoreLabel = Instantiate(multiScoreLabel, blockPosition, Quaternion.identity, scoreBar.transform);
                newScoreLabel.Initialize(amount, _currentScoreMultiplayer);
                if (_currentScoreMultiplayer < maxScoreMultiplayer)
                    _currentScoreMultiplayer++;
            }
            else
            {
                _currentScoreMultiplayer = 2;
                scoreBar.AddScore(amount);
                ScoreLabel newScoreLabel = Instantiate(scoreLabel, blockPosition, Quaternion.identity, scoreBar.transform);
                newScoreLabel.Initialize(amount);
            }
            _addScoreTime = Time.time;
        }
    }
}