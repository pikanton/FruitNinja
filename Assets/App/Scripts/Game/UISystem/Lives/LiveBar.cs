using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Game.UISystem.Lives
{
    public class LiveBar : MonoBehaviour
    {
        [SerializeField] private List<Live> liveList;
        [SerializeField] private float animationDuration = 0.2f;
        [SerializeField] [Range(0, 7)] private int liveCount = 5;
        public int CurrentLiveCount { get; private set; }

        private Animations _animations = new();

        public void Initialize()
        {
            CurrentLiveCount = liveCount;
            for (int i = 0; i < liveList.Count; i++)
            {
                liveList[i].transform.localScale = i < CurrentLiveCount ? Vector3.one : Vector3.zero;
            }
        }
        
        public void AddLive()
        {
            if (CurrentLiveCount < liveList.Count)
            {
                StartCoroutine(_animations.ScaleAnimation(liveList[CurrentLiveCount].transform, Vector3.one, animationDuration));
                CurrentLiveCount++;
            }
        }

        public void RemoveLive()
        {
            if (CurrentLiveCount > 0)
            {
                CurrentLiveCount--;
                StartCoroutine(_animations.ScaleAnimation(liveList[CurrentLiveCount].transform, Vector3.zero, animationDuration));
            }
        }

        private void OnValidate()
        {
            Initialize();
        }
    }
}