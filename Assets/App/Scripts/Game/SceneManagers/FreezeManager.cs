using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Game.SceneManagers
{
    public class FreezeManager : MonoBehaviour
    {
        [SerializeField] private float startValue = 0f;
        [SerializeField] private float endValue = 1f;
        [SerializeField] private float freezeDuration = 7f;
        [SerializeField] private Image frostImage;
        
        
        private bool _isFreeze;
        private Color _startColor;
        private Color _newColor;
        private float _currentAnimationTime = 0;
        
        public void Freeze()
        {
            _isFreeze = true;
            _currentAnimationTime = 0;
            _startColor = frostImage.color;
            _startColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0.7f);
            _newColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0f);
            SceneProperties.BlocksTimeScale = startValue;
        }
        
        private void Update()
        {
            if (_isFreeze)
            {
                if (_currentAnimationTime < freezeDuration)
                {
                    float progress = _currentAnimationTime / freezeDuration;
                    _currentAnimationTime += Time.deltaTime;
                    frostImage.color = Color.Lerp(_startColor, _newColor, progress);
                    SceneProperties.BlocksTimeScale = Mathf.Lerp(startValue, endValue, progress);
                }
                else
                {
                    SceneProperties.BlocksTimeScale = endValue;
                    frostImage.color = _newColor;
                    _isFreeze = false;
                }
            }
        }
    }
}