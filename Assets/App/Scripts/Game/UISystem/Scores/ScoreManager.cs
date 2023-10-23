using App.Scripts.Game.SceneManagers;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private ScoreBar scoreBar;
        [SerializeField] private ScoreLabel scoreLabel;
        [SerializeField] private MultiScoreLabel multiScoreLabel;
        [SerializeField] private float multiplyScoreDelay = 0.2f;
        [SerializeField] private int maxScoreMultiplayer = 4;
        
        private float _addScoreTime;
        private int _currentScoreMultiplayer;
        private int _currentSlicedBlocks;
        private int _lastAmount;
        private Vector3 _lastBlockPosition;

        private void Update()
        {
            if (_currentScoreMultiplayer >= 2 && Time.time >= _addScoreTime + multiplyScoreDelay)
            {
                scoreBar.AddScore(_lastAmount * _currentSlicedBlocks * (_currentScoreMultiplayer - 1));
                MultiScoreLabel newScoreLabel = Instantiate(multiScoreLabel, _lastBlockPosition, Quaternion.identity, scoreBar.transform);
                newScoreLabel.Initialize(_lastAmount, _currentScoreMultiplayer);
                _currentScoreMultiplayer = 0;
            }
        }
        
        public void AddScore(Vector3 blockPosition, int amount)
        {
            _lastAmount = amount;
            _lastBlockPosition = blockPosition;
            
            scoreBar.AddScore(amount);
            ScoreLabel newScoreLabel = Instantiate(scoreLabel, blockPosition, Quaternion.identity, scoreBar.transform);
            newScoreLabel.Initialize(amount);
            
            if (Time.time < _addScoreTime + multiplyScoreDelay)
            {
                _currentSlicedBlocks++;
                if (_currentScoreMultiplayer < maxScoreMultiplayer)
                    _currentScoreMultiplayer++;
            }
            else
            {
                _currentSlicedBlocks = 1;
                _currentScoreMultiplayer = 1;
            }
            
            _addScoreTime = Time.time;
        }
    }
}