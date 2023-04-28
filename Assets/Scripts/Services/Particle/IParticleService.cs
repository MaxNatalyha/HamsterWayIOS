using UnityEngine;

namespace Services
{
    public interface IParticleService
    {
        void PlayConfetti();
        void PlayStars(Vector3 firstPos, Vector3 secondPos);
    }
}