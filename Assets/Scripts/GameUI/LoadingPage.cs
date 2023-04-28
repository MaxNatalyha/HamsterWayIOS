using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class LoadingPage : PageUI, ILoadingPage
    {
        [SerializeField] private TMP_Text _loadDescriptionText;
        [SerializeField] private Image _loadingBar;
        
        public void UpdateProgress(string description, float progress)
        {
            _loadDescriptionText.text = description;
            _loadingBar.fillAmount = progress;
        }
    }
}
