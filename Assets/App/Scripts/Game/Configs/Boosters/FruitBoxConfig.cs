using App.Scripts.Game.Blocks.Models;
using UnityEngine;

namespace App.Scripts.Game.Configs.Boosters
{
    [CreateAssetMenu(fileName = "FruitBoxConfig", menuName = "Configs/Boosters/FruitBox")]
    public class FruitBoxConfig : ScriptableObject
    {
        public Fruit fruitPrefab;
        public int spawnFruitCount = 4;
        public float firstSpawnAngle = 50f;
        public float secondSpawnAngle = 130f;
        public float fruitBoxFruitInitialSpeed = 7f;
        public float fruitBoxImmortalityTime = 0.15f;
    }
}