using App.Scripts.Game.Animations;
using UnityEngine;

namespace App.Scripts.Game.Effects
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 0.5f;
        [SerializeField] private float animationDuration = 0.1f;
        [SerializeField] public SpriteRenderer spriteRenderer;
        
        private float _initialTime;
        private Color _initialColor;

        private readonly UIAnimation _uiAnimation = new();

        public void Initialize()
        {
            _initialTime = Time.time + lifeTime;
            _initialColor = spriteRenderer.color;
            transform.localScale = Vector3.zero;
            StartCoroutine(_uiAnimation.ScaleAnimation(transform, Vector3.one, animationDuration));
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            float elapsedTime = Time.time - (_initialTime - lifeTime);
            float alpha = 1f - (elapsedTime / lifeTime);
            spriteRenderer.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, alpha);
        }
    }
}