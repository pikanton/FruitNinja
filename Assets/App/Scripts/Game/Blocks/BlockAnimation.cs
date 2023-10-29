using System;
using System.Collections.Generic;
using App.Scripts.Game.Configs.Gameplay;
using App.Scripts.Game.SceneManagers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Game.Blocks
{
    public class BlockAnimation : MonoBehaviour
    {
        [SerializeField] private BlockAnimationConfig blockAnimationConfig;

        private Action _selectedAnimation;
        private Vector3 _initialScale;
        private float _targetScale;
        private float _scalingStartTime;
        private int _rotationDirection;
        private Dictionary<int, Action> _animationActions;

        public void Initialize()
        {
            _animationActions = new()
            {
                { 0, Rotate },
                { 1, Scale },
                { 2, () => { Scale(); Rotate(); } }
            };
            SetAnimationProperties();
            _initialScale = transform.localScale;
        }

        private void Update()
        {
            _selectedAnimation?.Invoke();
        }

        private void SetAnimationProperties()
        {
            _rotationDirection = Random.Range(0, 2) == 0 ? -1 : 1;
            _targetScale = Random.Range(0, 2) == 0 ? 
                blockAnimationConfig.targetIncreaseScale : blockAnimationConfig.targetDecreaseScale;
            int randomAnimationValue = Random.Range(0, _animationActions.Count);
            _selectedAnimation = _animationActions[randomAnimationValue];
            _scalingStartTime = Time.time;
        }
        
        private void Scale()
        {
            float elapsedTime = Time.time - _scalingStartTime;
            float scaleProgress = Mathf.Clamp01(elapsedTime / blockAnimationConfig.scalingDuration);

            Vector3 newScale = Vector3.Lerp(_initialScale, Vector3.one * _targetScale, scaleProgress);
            transform.localScale = newScale;
        }

        private void Rotate()
        {
            float finalRotationAngle = blockAnimationConfig.rotateAngle * _rotationDirection * 
                                       Time.deltaTime * SceneProperties.BlocksTimeScale;
            transform.Rotate(Vector3.forward * finalRotationAngle);
        }
    }
}