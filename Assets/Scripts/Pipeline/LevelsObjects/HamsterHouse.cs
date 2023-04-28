using System;
using SkinSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pipeline
{
    public class HamsterHouse : ConnectableObject, IPointerClickHandler
    {
        public Hamster Hamster => _hamster;

        [Header("House")]
        [SerializeField] private HamsterHousePipes _housePipes;
        [SerializeField] private Image _houseHoleImage;
        [SerializeField] private RectTransform _hamsterParent;
        [SerializeField] private Hamster _hamster;

        private Action _onPointerClickAction;

        private void Awake()
        {
            _connectors[0].OnConnectionChangeEvent += b => _housePipes.BottomPipe.gameObject.SetActive(b);
            _connectors[1].OnConnectionChangeEvent += b => _housePipes.LeftPipe.gameObject.SetActive(b);
            _connectors[2].OnConnectionChangeEvent += b => _housePipes.RightPipe.gameObject.SetActive(b);
        }

        public void BindAction(Action onClickAction)
        {
            _onPointerClickAction = onClickAction;
        }

        public override void SetUp(Vector2 size, Vector2 position, RectTransform newTopLayerParent, RectTransform parent)
        {
            base.SetUp(size, position, newTopLayerParent, parent);
            _hamster.SetUp(_hamsterParent);
            _hamster.gameObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(HasConnection())
                _onPointerClickAction?.Invoke();

            bool HasConnection()
            {
                return _connectors.Find(c=>c.IsConnected);
            }
        }

        public Connector[] GetHouseConnectors()
        {
            return _connectors.ToArray();
        }

        public void ApplySkin(ISkin houseSkin, ISkin pipesSkin)
        {
            var houseAsset = houseSkin.GetCurrentAssetVariant<HouseSkinAsset>();
        
            _viewImage.sprite = houseAsset.house;
            _houseHoleImage.sprite = houseAsset.houseHole;

            _viewImage.color = houseSkin.HasColorVariants ? houseSkin.GetCurrentColorVariant() : Color.white;

            _housePipes.ApplySkin(pipesSkin);
        }
    }
}

