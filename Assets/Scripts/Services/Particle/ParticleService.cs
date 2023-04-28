using UnityEngine;

namespace Services
{
    public class ParticleService : IParticleService
    {
        private readonly SceneParticles _sceneParticles;

        public ParticleService(SceneParticles sceneParticles)
        {
            _sceneParticles = sceneParticles;
        }

        public void PlayConfetti()
        {
            _sceneParticles.confetti.Play();
        }

        public void PlayStars(Vector3 firstPos, Vector3 secondPos)
        {
            _sceneParticles.starsFirst.transform.position = firstPos;
            _sceneParticles.starsSecond.transform.position = secondPos;

            _sceneParticles.starsFirst.Play();
            _sceneParticles.starsSecond.Play();
        }
    }
}
