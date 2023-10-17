using TMPro;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Scores
{
    public class ScoreLabel : MonoBehaviour
    {
        [SerializeField] private float animationDuration = 0.15f;
        [SerializeField] private float lifeTime = 1f;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private Animations _animations = new();
        private float _initialTime;
        private Color _initialColor;

        public void Initialize(int amount)
        {
            _initialTime = Time.time + lifeTime;
            _initialColor = textMeshPro.color;
            textMeshPro.text = amount.ToString();
            transform.localScale = Vector3.zero;
            Destroy(gameObject, lifeTime);
            StartCoroutine(_animations.ScaleAnimation(transform, Vector3.one, animationDuration));
        }

        private void Update()
        {
            float elapsedTime = Time.time - (_initialTime - lifeTime);
            float alpha = 1f - (elapsedTime / lifeTime);
            textMeshPro.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, alpha);
        }
    }
}