using App.Scripts.Game.Controllers;
using App.Scripts.Game.InputSystem;
using App.Scripts.Game.Spawners;
using App.Scripts.Game.UISystem;
using UnityEngine;

namespace App.Scripts.Game.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        public SpawnersController spawnersController;
        public BladeController bladeController;
        public LiveBar liveBar;
        private void Awake()
        {
            IInput inputController = GetInputController();
            bladeController.Initialize(inputController);
            liveBar.Initialize();
            spawnersController.Initialize();
        }

        private IInput GetInputController()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return new AndroidInputController();
            }
            else
            {
                return new WindowsInputController();
            }
        }
    }
}