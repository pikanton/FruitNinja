using System;
using App.Scripts.Game.Configs.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Game.Animations
{
    public class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private ButtonAnimationConfig buttonAnimationConfig;
        [SerializeField] private Button button;
        
        public Action ButtonAction;
        
        private static bool _isButtonEnabled = true;
        private bool _isThisButtonClick;
        private bool _isPointerDawn;
        private Vector3 _originalScale;
        private readonly UIAnimation _uiAnimation = new();
        
        private void Start()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isButtonEnabled) return;
            _isButtonEnabled = false;
            _isPointerDawn = true;
            StartCoroutine(_uiAnimation.ScaleAnimation(transform,
                _originalScale * buttonAnimationConfig.scaleMultiplier, buttonAnimationConfig.animationDuration));
            StartCoroutine(_uiAnimation.TintAnimation(button.image,
                buttonAnimationConfig.pressedTint, buttonAnimationConfig.animationDuration));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isPointerDawn) return;
            StartCoroutine(_uiAnimation.ScaleAnimation(transform,
                _originalScale, buttonAnimationConfig.animationDuration));
            StartCoroutine(_uiAnimation.TintAnimation(button.image,
                buttonAnimationConfig.initialTint, buttonAnimationConfig.animationDuration));
            _isButtonEnabled = true;
            _isPointerDawn = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isButtonEnabled)
                ButtonAction();
        }
    }
}