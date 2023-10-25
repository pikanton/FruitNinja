using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Game.Animations
{
    public class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        private static bool _isButtonEnabled = true;
        private bool _isThisButtonClick;
        private bool _isPointerDawn;
        
        [SerializeField] private Button button;
        [SerializeField] private float scaleMultiplier = 0.9f;
        [SerializeField] private float animationDuration = 0.1f;
        [SerializeField] private float initialTint = 1f;
        [SerializeField] private float pressedTint = 0.8f;

        public Action ButtonAction;

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
            StartCoroutine(_uiAnimation.ScaleAnimation(transform, _originalScale * scaleMultiplier, animationDuration));
            StartCoroutine(_uiAnimation.TintAnimation(button.image, pressedTint, animationDuration));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isPointerDawn) return;
            StartCoroutine(_uiAnimation.ScaleAnimation(transform, _originalScale, animationDuration));
            StartCoroutine(_uiAnimation.TintAnimation(button.image, initialTint, animationDuration));
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