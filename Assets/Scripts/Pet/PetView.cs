using System;
using System.Collections.Generic;
using Services;
using SkinSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pet
{
    public class PetView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PetPaws _petPaws;
        [SerializeField] private Image _desireFoodImage;

        private bool _isEating;
        private IPetFood _currentEatingFood;
        private PetStateEnum _currentState;
        private ISkinsService _skinsService;

        private readonly Dictionary<PetStateEnum, string> _animationDictionary = new Dictionary<PetStateEnum, string>()
        {
            { PetStateEnum.Loving, "Love" },
            { PetStateEnum.Running, "Run" },
            { PetStateEnum.Crying, "Cry" },
            { PetStateEnum.Eating, "Eat" },
            { PetStateEnum.Sleeping, "Sleep" },
            { PetStateEnum.Waiting, "Waiting" },
            { PetStateEnum.Sad, "Sad" },
            { PetStateEnum.Depressed, "Depressed" },
            { PetStateEnum.Angry, "Angry" },
            { PetStateEnum.Hungry, "Hungry" },
            { PetStateEnum.Sweet, "Sweet" },
            { PetStateEnum.Greeting, "Greeting" }
        };

        private Action _onEnableAction;

        [Inject]
        public void Construct(ISkinsService skinsService)
        {
            _skinsService = skinsService;
        }
        

        public void Initialize(Action onEnable)
        {
            _onEnableAction = onEnable;
        }

        public void PlayEatingAnimation(IPetFood food)
        {
            _isEating = true;
            _currentEatingFood = food;
            _animator.SetTrigger(_animationDictionary[PetStateEnum.Eating]);
        }

        public void PlayWaitingAnimation()
        {
            _animator.SetTrigger(_animationDictionary[PetStateEnum.Waiting]);
        }

        public void PlayHungryAnimation(Sprite desiredFoodImage)
        {
            _animator.SetTrigger(_animationDictionary[PetStateEnum.Hungry]);
            _desireFoodImage.sprite = desiredFoodImage;
        }

        public void PlayAnimationState(PetStateEnum state)
        {
            if (_desireFoodImage.enabled) _desireFoodImage.enabled = false;
            if (_isEating) return;

            _currentState = state;
            _animator.SetTrigger(_animationDictionary[state]);
        }

        public void OnBarClose()
        {
            PlayAnimationState(_currentState);
            _desireFoodImage.enabled = false;
        }

        private void OnEnable()
        {
            _onEnableAction?.Invoke();
            ApplySkin();
        }

        private void ANIMATION_HungryStart()
        {
            _desireFoodImage.enabled = true;
        }

        private void ANIMATION_HungryFinish()
        {
            _desireFoodImage.enabled = false;
        }

        private void ANIMATION_EatingStart()
        {
            _petPaws.StartEating(_currentEatingFood.Config.FoodSprite);
        }

        private void ANIMATION_EatingFinish()
        {
            _isEating = false;
            PlayAnimationState(_currentState);
        }

        private void ApplySkin()
        {
            var petSkin = _skinsService.GetSkin(SkinCategories.Pet);
            var petAsset = petSkin.GetCurrentAssetVariant<PetSkinAsset>();

            _animator.runtimeAnimatorController = petAsset.animator;
            _petPaws.ApplySkin(petAsset);        
        }
    }
}
