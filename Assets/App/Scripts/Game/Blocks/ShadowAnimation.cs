using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class ShadowAnimation : MonoBehaviour
    {
        public Transform blockTransform;
        public SpriteRenderer blockSpriteRenderer;
        public SpriteRenderer shadowSpriteRenderer;

        private Vector3 _initialShadowOffset;
        private Vector3 _initialBlockScale;

        private void Start()
        {
            CopySpriteFromBlock();
            InitializeInitialValues();
        }
        
        private void Update()
        {
            UpdateScale();
            UpdatePosition();
            UpdateRotation();
        }
        
        private void CopySpriteFromBlock()
        {
            shadowSpriteRenderer.sprite = blockSpriteRenderer.sprite;
        }

        private void InitializeInitialValues()
        {
            _initialShadowOffset = transform.localPosition;
            _initialBlockScale = blockTransform.localScale;
        }
        
        private void UpdateScale()
        {
            Vector3 fruitScale = blockTransform.localScale;
            transform.localScale = fruitScale;
        }

        private void UpdatePosition()
        {
            Vector3 newOffset = CalculateNewPosition();
            transform.localPosition = newOffset;
        }

        private Vector3 CalculateNewPosition()
        {
            Vector3 fruitScale = blockTransform.localScale;
            Vector3 newOffset = new Vector3(
                _initialShadowOffset.x + fruitScale.x - _initialBlockScale.x,
                _initialShadowOffset.y - fruitScale.y + _initialBlockScale.y
                );
            return newOffset;
        }

        private void UpdateRotation()
        {
            transform.rotation = blockTransform.rotation;
        }
    }
}