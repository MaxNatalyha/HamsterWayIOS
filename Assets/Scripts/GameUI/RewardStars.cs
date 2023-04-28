using System;
using System.Collections;
using Services;
using UnityEngine;
using Zenject;

namespace GameUI
{
    public class RewardStars : MonoBehaviour
    {
        [SerializeField] private Animator[] _starsAnimators = new Animator[3];

        private IAudioService _audioService;
        
        private const string _animBool = "Show";

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void SetWithAnimation(int count)
        {
            count = Math.Clamp(count, 0, 3);

            StartCoroutine(AnimationRoutine(count));
        }

        private IEnumerator AnimationRoutine(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _starsAnimators[i].SetBool(_animBool, true);
                _audioService.PlaySound(AudioTypes.StarReward);
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
