using UnityEngine;
using System.Collections.Generic;

namespace App.Scripts.Game.Block
{
    public class BlockAnimation : MonoBehaviour
    {
        public List<Sprite> blockSprites;
        public float rotateAngle = 2.0f;

        public float targetIncreaseScale = 1.3f;
        public float targetDecreaseScale = 0.8f;
        public float scalingDuration = 4.0f;

        private Vector3 _initialScale;
        private float _targetScale;
        private float _scalingStartTime;
        private int _rotationDirection;
        private int _randomAnimationValue;

        private void Start()
        {
            SetRandomBlockSprite();
            SetAnimationProperties();
            
            _initialScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            PlayAnimation();
        }

        private void SetAnimationProperties()
        {
            _rotationDirection = Random.Range(0, 2) == 0 ? -1 : 1;
            _targetScale = Random.Range(0, 2) == 0 ? targetIncreaseScale : targetDecreaseScale;
            _randomAnimationValue = Random.Range(0, 3);
            _scalingStartTime = Time.time;
        }
        
        private void SetRandomBlockSprite()
        {
            int randomSpriteIndex = Random.Range(0, blockSprites.Count);
            Sprite selectedSprite = blockSprites[randomSpriteIndex];
            gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }

        private void Scale()
        {
            float elapsedTime = Time.time - _scalingStartTime;
            float scaleProgress = Mathf.Clamp01(elapsedTime / scalingDuration);

            Vector3 newScale = Vector3.Lerp(_initialScale, Vector3.one * _targetScale, scaleProgress);
            transform.localScale = newScale;
        }

        private void Rotate()
        {
            float finalRotationAngle = rotateAngle * _rotationDirection;
            transform.Rotate(Vector3.forward * finalRotationAngle);
        }

        private void PlayAnimation()
        {
            switch (_randomAnimationValue)
            {
                case 0:
                    Rotate();
                    break;
                case 1:
                    Scale();
                    break;
                case 2:
                    Scale();
                    Rotate();
                    break;
            }
        }
    }
}