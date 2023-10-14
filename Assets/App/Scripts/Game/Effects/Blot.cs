using UnityEngine;

namespace App.Scripts.Game.Effects
{
    public class Blot : MonoBehaviour
    {
        public float lifeTime = 6f;
        public SpriteRenderer spriteRenderer;
        private float _initialTime;
        private Color _initialColor;

        public void Initialize()
        {
            _initialTime = Time.time + lifeTime;
            _initialColor = spriteRenderer.color;
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