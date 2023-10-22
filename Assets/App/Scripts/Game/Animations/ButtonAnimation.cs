using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Game.Animations
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private float scaleMultiplier = 0.9f;
        [SerializeField] private float animationDuration = 0.1f;

        private Vector3 _originalScale;
        private UIAnimation _uiAnimation = new();
        private void Start()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(_uiAnimation.ScaleAnimation(transform, _originalScale * scaleMultiplier, animationDuration));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StartCoroutine(_uiAnimation.ScaleAnimation(transform, _originalScale, animationDuration));
        }
    }
}