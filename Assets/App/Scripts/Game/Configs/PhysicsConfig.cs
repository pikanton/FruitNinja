﻿using UnityEngine;

namespace App.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "PhysicsConfig", menuName = "Configs/Physics")]
    public class PhysicsConfig : ScriptableObject
    {
        public float initialSpeed = 13f;
        public float gravity = 9.8f;
    }
}