using App.Scripts.Game.Blades;
using App.Scripts.Game.InputSystem;
using App.Scripts.Game.Spawners;
using App.Scripts.Game.UISystem.Lives;
using App.Scripts.Game.UISystem.Scores;
using UnityEngine;
using AndroidInput = App.Scripts.Game.InputSystem.AndroidInput;

namespace App.Scripts.Game.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private SpawnersManager spawnersManager;
        [SerializeField] private BladeMovement bladeMovement;
        [SerializeField] private LiveBar liveBar;
        [SerializeField] private ScoreBar scoreBar;
        private void Awake()
        {
            IInput inputController = GetInputController();
            bladeMovement.Initialize(inputController);
            liveBar.Initialize();
            scoreBar.Initialize();
            spawnersManager.Initialize();
        }

        private IInput GetInputController()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return new AndroidInput();
            }
            else
            {
                return new WindowsInput();
            }
        }
    }
}