using TMPro;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class MultiScoreLabel : MonoBehaviour
    {
        [SerializeField] private float animationDuration = 0.15f;
        [SerializeField] private float lifeTime = 1f;
        [SerializeField] private TextMeshProUGUI fruits;
        [SerializeField] private TextMeshProUGUI series;
        [SerializeField] private TextMeshProUGUI multiplayer;
        private const string FruitFormat = " fruits";
        private const string MultiplayerFormat = "x";

        private Animations _animations = new();
        private float _initialTime;
        private Color _initialColorFruits;
        private Color _initialColorSeries;
        private Color _initialColorMultiplayer;

        public void Initialize(int amount, int currentScoreMultiPlayer)
        {
            _initialTime = Time.time + lifeTime;
            _initialColorFruits = fruits.color;
            _initialColorSeries = series.color;
            _initialColorMultiplayer = multiplayer.color;
            fruits.text = currentScoreMultiPlayer.ToString() + FruitFormat;
            multiplayer.text = MultiplayerFormat + currentScoreMultiPlayer.ToString();
            transform.localScale = Vector3.zero;
            Destroy(gameObject, lifeTime);
            StartCoroutine(_animations.ScaleAnimation(transform, Vector3.one, animationDuration));
        }

        private void Update()
        {
            float elapsedTime = Time.time - (_initialTime - lifeTime);
            float alpha = 1f - (elapsedTime / lifeTime);
            fruits.color = GetNewAlphaColor(_initialColorFruits, alpha);
            series.color = GetNewAlphaColor(_initialColorSeries, alpha);
            multiplayer.color = GetNewAlphaColor(_initialColorMultiplayer, alpha);
        }

        private Color GetNewAlphaColor(Color initialColor, float alpha)
        {
            return new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
        }
    }
}