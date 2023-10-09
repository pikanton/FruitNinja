using UnityEngine;
using App.Scripts.Game.Spawners;

namespace App.Scripts.Game.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        public SpawnersManager spawnersManager;
        private void Awake()
        {
            spawnersManager.Initialize();
        }
    }
}