using App.Scripts.Game.Animations;
using App.Scripts.Game.Configs.UI;
using TMPro;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class ScoreLabel : MonoBehaviour
    {
        [SerializeField] private ScoreLabelConfig scoreLabelConfig;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private readonly UIAnimation _uiAnimation = new();
        private float _initialTime;
        private Color _initialColor;

        public void Initialize(int amount)
        {
            _initialTime = Time.time + scoreLabelConfig.lifeTime;
            _initialColor = textMeshPro.color;
            textMeshPro.text = amount.ToString();
            transform.localScale = Vector3.zero;
            Destroy(gameObject, scoreLabelConfig.lifeTime);
            StartCoroutine(_uiAnimation.ScaleAnimation(transform,
                Vector3.one, scoreLabelConfig.animationDuration));
        }

        private void Update()
        {
            float elapsedTime = Time.time - (_initialTime - scoreLabelConfig.lifeTime);
            float alpha = 1f - (elapsedTime / scoreLabelConfig.lifeTime);
            textMeshPro.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, alpha);
        }
    }
}