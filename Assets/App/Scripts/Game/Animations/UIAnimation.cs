﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Game.Animations
{
    public class UIAnimation
    {
        public IEnumerator ScaleAnimation(Transform targetTransform, Vector3 targetScale, float animationDuration)
        {
            Vector3 startScale = targetTransform.localScale;
    
            float currentAnimationTime = 0;
            while (currentAnimationTime < animationDuration)
            {
                float progress = currentAnimationTime / animationDuration;
                currentAnimationTime += Time.unscaledDeltaTime;
                targetTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);
                yield return null;
            }
            targetTransform.localScale = targetScale;
        }

        public IEnumerator AnimateValueChange(int startValue, int amount, float animationDuration,
            TextMeshProUGUI textMeshPro, string prefix = "")
        {
            int targetValue = startValue + amount;
    
            float currentAnimationTime = 0;
            while (currentAnimationTime < animationDuration)
            {
                float progress = currentAnimationTime / animationDuration;
                currentAnimationTime += Time.unscaledDeltaTime;
                var displayValue = (int)Mathf.Lerp(startValue, targetValue, progress);
                textMeshPro.text = $"{prefix}{displayValue.ToString()}";
                yield return null;
            }
            textMeshPro.text = $"{prefix}{targetValue.ToString()}";
        }

        public IEnumerator FadeAnimation(Image image, float newAlpha, float animationDuration)
        {
            Color startColor = image.color;
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
    
            float currentAnimationTime = 0;
            while (currentAnimationTime < animationDuration)
            {
                float progress = currentAnimationTime / animationDuration;
                currentAnimationTime += Time.unscaledDeltaTime;
                image.color = Color.Lerp(startColor, newColor, progress);
                yield return null;
            }

            image.color = newColor;
        }
        
        public IEnumerator FadeScaledAnimation(Image image, float newAlpha, float animationDuration)
        {
            Color startColor = image.color;
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
    
            float currentAnimationTime = 0;
            while (currentAnimationTime < animationDuration)
            {
                float progress = currentAnimationTime / animationDuration;
                currentAnimationTime += Time.deltaTime;
                image.color = Color.Lerp(startColor, newColor, progress);
                yield return null;
            }

            image.color = newColor;
        }
        
        public IEnumerator TintAnimation(Image image, float newTint, float animationDuration)
        {
            Color startColor = image.color;
            Color newColor = new Color(newTint, newTint, newTint, startColor.a);
    
            float currentAnimationTime = 0;
            while (currentAnimationTime < animationDuration)
            {
                float progress = currentAnimationTime / animationDuration;
                currentAnimationTime += Time.unscaledDeltaTime;
                image.color = Color.Lerp(startColor, newColor, progress);
                yield return null;
            }

            image.color = newColor;
        }
        
        public IEnumerator DoActionAfterDelay(Action action, float timeDelay)
        {
            float currentAnimationTime = 0;
            while (currentAnimationTime < timeDelay)
            {
                currentAnimationTime += Time.unscaledDeltaTime;
                yield return null;
            }
            action();
        }
    }
}