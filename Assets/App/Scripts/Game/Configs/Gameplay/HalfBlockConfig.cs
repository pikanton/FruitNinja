using UnityEngine;

namespace App.Scripts.Game.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "HalfBlockConfig", menuName = "Configs/Gameplay/HalfBlock")]
    public class HalfBlockConfig : ScriptableObject
    {
        public float halfBlockInitialSpeed = 6f;
    }
}