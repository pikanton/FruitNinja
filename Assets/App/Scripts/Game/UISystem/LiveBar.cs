using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Game.UISystem
{
    public class LiveBar : MonoBehaviour
    {
        public List<Live> liveList;
        [Range(0, 7)] public int liveCount = 3;
        public int CurrentLiveCount { get; private set; }

        public void Initialize()
        {
            CurrentLiveCount = liveCount;
            for (int i = 0; i < liveList.Count; i++)
            {
                if (i < CurrentLiveCount)
                    liveList[i].image.enabled = true;
                else 
                    liveList[i].image.enabled = false;
            }
        }
        
        public void AddLive()
        {
            liveList[CurrentLiveCount++].image.enabled = true;
        }

        public void RemoveLive()
        {
            liveList[--CurrentLiveCount].image.enabled = false;
        }
        
        private void OnValidate()
        {
            Initialize();
        }
    }
}