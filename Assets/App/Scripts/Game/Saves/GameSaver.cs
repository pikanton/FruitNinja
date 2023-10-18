using UnityEngine;

namespace App.Scripts.Game.Saves
{
    public class GameSaver
    {
        private string _highScoreKey = "HighScore";
        
        public void SaveHighScore(int highScore)
        {
            PlayerPrefs.SetInt(_highScoreKey, highScore);
        }

        public int GetHighScore()
        {
            return PlayerPrefs.GetInt(_highScoreKey);

        }
    }
}