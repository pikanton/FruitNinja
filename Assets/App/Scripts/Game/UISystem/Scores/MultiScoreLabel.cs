using App.Scripts.Game.Animations;
using App.Scripts.Game.Configs.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Game.UISystem.Scores
{
    public class MultiScoreLabel : MonoBehaviour
    {
        [SerializeField] private MultiScoreLabelConfig multiScoreLabelConfig;
        [SerializeField] private TextMeshProUGUI fruits;
        [SerializeField] private TextMeshProUGUI series;
        [SerializeField] private TextMeshProUGUI multiplayer;

        private readonly UIAnimation _uiAnimation = new();
        private float _initialTime;
        private Color _initialColorFruits;
        private Color _initialColorSeries;
        private Color _initialColorMultiplayer;

        public void Initialize(int amount, int currentScoreMultiPlayer)
        {
            _initialTime = Time.time + multiScoreLabelConfig.lifeTime;
            _initialColorFruits = fruits.color;
            _initialColorSeries = series.color;
            _initialColorMultiplayer = multiplayer.color;
            fruits.text = currentScoreMultiPlayer.ToString() + multiScoreLabelConfig.fruitFormat;
            multiplayer.text = multiScoreLabelConfig.multiplayerFormat + currentScoreMultiPlayer.ToString();
            transform.localScale = Vector3.zero;
            Destroy(gameObject, multiScoreLabelConfig.lifeTime);
            StartCoroutine(_uiAnimation.ScaleAnimation(transform,
                Vector3.one, multiScoreLabelConfig.animationDuration));
        }

        private void Update()
        {
            float elapsedTime = Time.time - (_initialTime - multiScoreLabelConfig.lifeTime);
            float alpha = 1f - (elapsedTime / multiScoreLabelConfig.lifeTime);
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