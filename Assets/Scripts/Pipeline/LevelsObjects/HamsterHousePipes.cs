using SkinSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Pipeline
{
    public class HamsterHousePipes : MonoBehaviour
    {
        public RectTransform BottomPipe => _bottomPipeBack.rectTransform;
        public RectTransform LeftPipe => _leftPipeBack.rectTransform;
        public RectTransform RightPipe => _rightPipeBack.rectTransform; 
    
        [SerializeField] private Image _bottomPipeBack;
        [SerializeField] private Image _leftPipeBack;
        [SerializeField] private Image _rightPipeBack;
        [SerializeField] private Image _bottomPipeTop;
        [SerializeField] private Image _leftPipeTop;
        [SerializeField] private Image _rigthPipeTop;

        private void OnDisable()
        {
            BottomPipe.gameObject.SetActive(false);
            LeftPipe.gameObject.SetActive(false);
            RightPipe.gameObject.SetActive(false);
        }

        public void ApplySkin(ISkin pipesSkin)
        {
            var pipesAsset = pipesSkin.GetCurrentAssetVariant<PipesSkinAsset>();
        
            _bottomPipeTop.sprite = pipesAsset.straightPipe;
            _leftPipeTop.sprite = pipesAsset.housePipe;
            _rigthPipeTop.sprite = pipesAsset.housePipe;

            _bottomPipeBack.color = pipesAsset.backColor;
            _leftPipeBack.color = pipesAsset.backColor;
            _rightPipeBack.color = pipesAsset.backColor;
            
   
            _bottomPipeTop.color = pipesSkin.HasColorVariants ? pipesSkin.GetCurrentColorVariant() : Color.white;
            _leftPipeTop.color = pipesSkin.HasColorVariants ? pipesSkin.GetCurrentColorVariant() : Color.white;
            _rigthPipeTop.color = pipesSkin.HasColorVariants ? pipesSkin.GetCurrentColorVariant() : Color.white;
        }
    }
}
