using App.Scripts.Game.Configs.Boosters;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace App.Scripts.Game.SceneManagers
{
    public class FreezeManager : MonoBehaviour
    {
        [SerializeField] private FreezerConfig freezerConfig;
        [SerializeField] private Image frostEffectImage;
        
        private bool _isFreeze;
        private Color _startColor;
        private Color _newColor;
        private float _currentAnimationTime;
        
        public void Freeze()
        {
            _isFreeze = true;
            _currentAnimationTime = 0;
            _startColor = frostEffectImage.color;
            _startColor = new Color(_startColor.r, _startColor.g, _startColor.b, freezerConfig.startFreezeImageAlpha);
            _newColor = new Color(_startColor.r, _startColor.g, _startColor.b, freezerConfig.endFreezeImageAlpha);
            SceneProperties.BlocksTimeScale = freezerConfig.startFreezeValue;
        }
        
        private void Update()
        {
            if (_isFreeze)
            {
                if (_currentAnimationTime < freezerConfig.freezeDuration)
                {
                    float progress = _currentAnimationTime / freezerConfig.freezeDuration;
                    _currentAnimationTime += Time.deltaTime;
                    frostEffectImage.color = Color.Lerp(_startColor, _newColor, progress);
                    SceneProperties.BlocksTimeScale = Mathf.Lerp(freezerConfig.startFreezeValue,
                        freezerConfig.endFreezeValue, progress);
                }
                else
                {
                    SceneProperties.BlocksTimeScale = freezerConfig.endFreezeValue;
                    frostEffectImage.color = _newColor;
                    _isFreeze = false;
                }
            }
        }
    }
}