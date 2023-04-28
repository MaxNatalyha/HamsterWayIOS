using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pipeline
{
    public class SelectionPageUpperPanel : MonoBehaviour
    {
        public TMP_Text StarsAmountText => _starsAmountText;
        public Button BackButton => _backButton;
        
        [SerializeField] private TMP_Text _starsAmountText;
        [SerializeField] private Button _backButton;
    }
}