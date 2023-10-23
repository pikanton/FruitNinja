using System.Collections.Generic;
using App.Scripts.Game.Animations;
using App.Scripts.Game.Configs;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Lives
{
    public class LiveBar : MonoBehaviour
    {
        [SerializeField] private List<Live> liveList;
        [SerializeField] private LivesConfig livesConfig;

        public int CurrentLiveCount { get; private set; }

        private readonly UIAnimation _uiAnimation = new();
    
        public void Initialize()
        {
            CurrentLiveCount = livesConfig.liveCount;
            for (int i = 0; i < liveList.Count; i++)
            {
                liveList[i].transform.localScale = i < CurrentLiveCount ? Vector3.one : Vector3.zero;
            }
        }
        
        public void AddLive()
        {
            if (CurrentLiveCount < liveList.Count)
            {
                StartCoroutine(_uiAnimation.ScaleAnimation(liveList[CurrentLiveCount].transform, Vector3.one, livesConfig.animationDuration));
                CurrentLiveCount++;
            }
        }

        public void RemoveLive()
        {
            if (CurrentLiveCount > 0)
            {
                CurrentLiveCount--;
                StartCoroutine(_uiAnimation.ScaleAnimation(liveList[CurrentLiveCount].transform, Vector3.zero, livesConfig.animationDuration));
            }
        }

        private void OnValidate()
        {
            Initialize();
        }
    }
}