using UnityEngine;
using UnityEngine.UI;

namespace SettingsScripts
{
    public class SettingsButton : MonoBehaviour
    {
        public SettingsEnum setting;
    
        [SerializeField] private Image _turnOffIcon;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            _turnOffIcon.enabled = !Settings.GetSetting(setting);
        }

        private void OnClick()
        {
            Settings.SetSetting(setting, !Settings.GetSetting(setting));
            _turnOffIcon.enabled = !Settings.GetSetting(setting);
        }
    }
}
