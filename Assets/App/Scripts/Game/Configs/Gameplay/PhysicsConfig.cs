using UnityEngine;

namespace App.Scripts.Game.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "PhysicsConfig", menuName = "Configs/Gameplay/Physics")]
    public class PhysicsConfig : ScriptableObject
    {
        public float gravity = 9.8f;
        public float velocityLimit = 13f;
    }
}