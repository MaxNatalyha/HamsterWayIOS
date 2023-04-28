using Services;
using SkinSystem;
using StateMachineCore;

namespace Shop.SkinsSection.SkinViewStates
{
    public class SelectState : IState
    {
        private readonly ISkinsService _skinsService;
        private readonly SkinShowcaseView _skinShowcaseView;
        private readonly DisplayedSkinInfo _displayedSkinInfo;

        public SelectState(SkinShowcaseView skinShowcase, ISkinsService skinsService, DisplayedSkinInfo displayedSkinInfo)
        {
            _skinShowcaseView = skinShowcase;
            _skinsService = skinsService;
            _displayedSkinInfo = displayedSkinInfo;
        }
        
        public void Enter()
        {
            bool isSelected = CheckSelection(_displayedSkinInfo.Skin, _displayedSkinInfo.VariantId);
            
            _skinShowcaseView.selectedPanel.gameObject.SetActive(isSelected);
            _skinShowcaseView.selectButton.gameObject.SetActive(!isSelected);
        }

        public void Exit()
        {
            _skinShowcaseView.selectedPanel.gameObject.SetActive(false);
            _skinShowcaseView.selectButton.gameObject.SetActive(false);
        }
        
        private bool CheckSelection(ISkin skin, int assetId)
        {
            return _skinsService.SelectedSkins[skin.Category] == skin.SkinName && skin.SelectedVariantId == assetId;
        }
    }
}