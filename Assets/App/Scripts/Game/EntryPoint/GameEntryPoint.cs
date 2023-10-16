using App.Scripts.Game.Controllers;
using App.Scripts.Game.InputSystem;
using App.Scripts.Game.Spawners;
using App.Scripts.Game.UISystem;
using UnityEngine;

namespace App.Scripts.Game.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private SpawnersController spawnersController;
        [SerializeField] private BladeController bladeController;
        [SerializeField] private LiveBar liveBar;
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