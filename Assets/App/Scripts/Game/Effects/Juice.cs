using UnityEngine;

namespace App.Scripts.Game.Effects
{
    public class Juice : MonoBehaviour
    {
        public ParticleSystem juiceParticle;

        public void Initialize(Color color)
        {
            juiceParticle.Stop();
            var mainModule = juiceParticle.main;
            mainModule.startColor = color;
            juiceParticle.Play();
            float lifeTime = mainModule.duration;
            Destroy(gameObject, lifeTime);
        }
    }
}