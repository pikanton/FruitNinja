using App.Scripts.Game.Configs.Effects;
using UnityEngine;

namespace App.Scripts.Game.Effects
{
    public class Blot : MonoBehaviour
    {
        [SerializeField] private BlotConfig blotConfig;
        [SerializeField] public SpriteRenderer spriteRenderer;
        
        private float _initialTime;
        private Color _initialColor;

        public void Initialize()
        {
            _initialTime = Time.time + blotConfig.lifeTime;
            _initialColor = spriteRenderer.color;
            Destroy(gameObject, blotConfig.lifeTime);
        }

        private void Update()
        {
            float elapsedTime = Time.time - (_initialTime - blotConfig.lifeTime);
            float alpha = 1f - (elapsedTime / blotConfig.lifeTime);
            spriteRenderer.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, alpha);
        }
    }
}