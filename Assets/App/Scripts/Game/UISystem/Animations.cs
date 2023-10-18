using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace App.Scripts.Game.UISystem
{
    public class Animations
    {
        public event Action OnScaleAnimationComplete;
        public IEnumerator ScaleAnimation(Transform targetTransform, Vector3 targetScale, float animationDuration)
        {
            float elapsedTime = 0;
            Vector3 startScale = targetTransform.localScale;

            while (elapsedTime < animationDuration)
            {
                targetTransform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            targetTransform.localScale = targetScale;
            OnScaleAnimationComplete?.Invoke(); 
        }
        
        public IEnumerator AnimateValueChange(int startValue, int amount, float valueAnimationDuration,
            TextMeshProUGUI textMeshPro, string prefix = "")
        {
            int targetValue = startValue + amount;
            float startTime = Time.time;
            float endTime = startTime + valueAnimationDuration;

            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / valueAnimationDuration;
                var displayValue = (int)Mathf.Lerp(startValue, targetValue, progress);
                textMeshPro.text = $"{prefix}{displayValue.ToString()}";
                yield return null;
            }
            textMeshPro.text = $"{prefix}{targetValue.ToString()}";
        }
    }
}