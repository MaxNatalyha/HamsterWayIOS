using System;
using SkinSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.SkinsSection
{
    public class SkinVariantView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _variantImage, _borderImage;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private RectTransform _lockPanel;
        [SerializeField] private Image _bougthImage;
    
        private int _variantId;

        public void Setup(ISkinAsset assetInfo, int id, RectTransform parent, Action<int> onClickAction, bool isLock)
        {
            _button.onClick.AddListener(() => onClickAction?.Invoke(_variantId));
            _variantImage.color = assetInfo.PreviewColor;
            _variantId = id;

            _lockPanel.gameObject.SetActive(id!=0 && isLock);
            _bougthImage.enabled = assetInfo.Bought;
            
            transform.SetParent(parent);
            gameObject.SetActive(true);
        }

        public void Refresh(ISkinAsset assetInfo, int id, bool isLock)
        {
            _lockPanel.gameObject.SetActive(id!=0 && isLock);
            _bougthImage.enabled = assetInfo.Bought;
        }
        
        public void CleanUp()
        {
            _button.onClick.RemoveAllListeners();
            _borderImage.color = Color.black;
            
            _lockPanel.gameObject.SetActive(false);
            _bougthImage.enabled = false;
        }

        public void OnSelectedVariantChange(int id) => _borderImage.color = id == _variantId ? _selectedColor : Color.black;
    }
}
