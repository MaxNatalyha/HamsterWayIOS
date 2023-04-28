using System;
using SkinSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MatchingCards
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public bool IsMatch { get; set; }
        public int Id { get; private set; }
    
        [SerializeField] private Image _cardImage;
        [SerializeField] private Image _cardFaceImage;
        [SerializeField] private Sprite _cardFace;
        [SerializeField] private Sprite _cardBack;
        [SerializeField] private Animator _animator;
        [SerializeField] private RectTransform _rectTransform;

        private const string OPEN_KEY = "Open";
        
        private Action<Card> _onOpenAction;

        private void Awake() => _animator.keepAnimatorStateOnDisable = true;

        public void Setup(Action<Card> onOpen, Vector2 position, Vector2 size, RectTransform parent)
        {
            _rectTransform.anchoredPosition = position;
            _rectTransform.sizeDelta = size;
            _onOpenAction = onOpen;
            
            _rectTransform.SetParent(parent);
            gameObject.SetActive(true);
        }

        public void SetPair(CardMatchPair currentPair)
        {
            Id = currentPair.Id;
            _cardFaceImage.sprite = currentPair.Sprite;
            
            name = $"Card {currentPair.Id}";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!IsMatch)
                OnCardClick();
        }

        public void CloseCard()
        {
            _animator.SetBool(OPEN_KEY, false);
        }

        public void CleanUp()
        {
            IsMatch = false;
        }

        public void ApplySkin(ISkin cardSkin)
        {
            var cardAsset = cardSkin.GetCurrentAssetVariant<CardSkinAsset>();
        
            _cardBack = cardAsset.cardBack;
            _cardImage.sprite = _cardBack;

            _cardImage.color = cardSkin.HasColorVariants ? cardSkin.GetCurrentColorVariant() : Color.white;
        }

        private void OnCardClick()
        {
            _animator.SetBool(OPEN_KEY, true);
        }

        private void ANIMATOR_FlipToBack()
        {
            _cardImage.sprite = _cardBack;
            _cardFaceImage.enabled = false;
        }

        private void ANIMATOR_FlipToFace()
        {
            _cardImage.sprite = _cardFace;
            _cardFaceImage.enabled = true;
        }

        private void ANIMATOR_OnOpenCard()
        {
            _onOpenAction?.Invoke(this);
        }
    }
}
