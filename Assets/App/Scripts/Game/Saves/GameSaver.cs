using UnityEngine;

namespace App.Scripts.Game.Saves
{
    public class GameSaver
    {
        private const string HighScoreKey = "HighScore";

        public void SaveHighScore(int highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, highScore);
        }

        public int GetHighScore()
        {
            return PlayerPrefs.GetInt(HighScoreKey);

        }
    }
}