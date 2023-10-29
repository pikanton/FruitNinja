using App.Scripts.Game.Animations;
using App.Scripts.Game.Configs.Effects;
using UnityEngine;

namespace App.Scripts.Game.Effects
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private EffectConfig effectConfig;
        [SerializeField] public SpriteRenderer spriteRenderer;
        
        private float _initialTime;
        private Color _initialColor;

        private readonly UIAnimation _uiAnimation = new();

        public void Initialize()
        {
            _initialTime = Time.time + effectConfig.lifeTime;
            _initialColor = spriteRenderer.color;
            transform.localScale = Vector3.zero;
            StartCoroutine(_uiAnimation.ScaleAnimation(transform,
                Vector3.one, effectConfig.scaleAnimationDuration));
            Destroy(gameObject, effectConfig.lifeTime);
        }

        private void Update()
        {
            float elapsedTime = Time.time - (_initialTime - effectConfig.lifeTime);
            float alpha = 1f - (elapsedTime / effectConfig.lifeTime);
            spriteRenderer.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, alpha);
        }
    }
}