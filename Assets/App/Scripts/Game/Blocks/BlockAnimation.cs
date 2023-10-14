using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Blocks
{
    public class BlockAnimation : MonoBehaviour
    {
        public bool disableScaling = false;
        
        public float rotateAngle = 2.0f;

        public float targetIncreaseScale = 1.3f;
        public float targetDecreaseScale = 0.8f;
        public float scalingDuration = 4.0f;

        private Vector3 _initialScale;
        private float _targetScale;
        private float _scalingStartTime;
        private int _rotationDirection;
        private int _randomAnimationValue;
        private Dictionary<int, Action> animationActions = new Dictionary<int, Action>();

        public void Initialize()
        {
            SetAnimationProperties();
            animationActions.Add(0, Rotate);
            animationActions.Add(1, () => { Scale(); Rotate(); });
            if (disableScaling) animationActions.Add(2, Scale); 
            _initialScale = transform.localScale;
        }

        private void Update()
        {
            PlayAnimation();
        }

        private void SetAnimationProperties()
        {
            _rotationDirection = Random.Range(0, 2) == 0 ? -1 : 1;
            _targetScale = Random.Range(0, 2) == 0 ? targetIncreaseScale : targetDecreaseScale;
            _randomAnimationValue = Random.Range(0, animationActions.Count);
            _scalingStartTime = Time.time;
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
            float finalRotationAngle = rotateAngle * _rotationDirection * Time.deltaTime;
            transform.Rotate(Vector3.forward * finalRotationAngle);
        }

        private void PlayAnimation()
        {
            if (animationActions.TryGetValue(_randomAnimationValue, out var action))
            {
                action();
            }
        }
    }
}