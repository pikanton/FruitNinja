using UnityEngine;

namespace App.Scripts.Game.Configs.Boosters
{
    [CreateAssetMenu(fileName = "BombConfig", menuName = "Configs/Boosters/Bomb")]
    public class BombConfig : ScriptableObject
    {
        public float explosionForce = 15f;
    }
}