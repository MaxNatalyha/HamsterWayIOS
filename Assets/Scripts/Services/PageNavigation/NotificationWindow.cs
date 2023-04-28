using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class NotificationWindow : MonoBehaviour, INotificationWindow
    {
        [SerializeField] private Text _contentText;
        [SerializeField] private Image _previewImage;
        [SerializeField] private Button _closeButton, _actionButton;

        private const float DEFAULT_ACTIVE_TIME = 2f;
        
        public void Open(string content, Sprite icon)
        {
            _previewImage.sprite = icon;
            _contentText.text = content;
            
            gameObject.SetActive(true);
            StartCoroutine(CloseRoutine());
        }

        public void Open(string content, Sprite icon, Action action)
        {
            _actionButton.onClick.AddListener(() => action?.Invoke());
            _actionButton.gameObject.SetActive(true);
            _previewImage.sprite = icon;
            _contentText.text = content;
            
            gameObject.SetActive(true);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
            _actionButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            Clean();
        }

        private IEnumerator CloseRoutine()
        {
            yield return new WaitForSeconds(DEFAULT_ACTIVE_TIME);
            Close();
        }

        private void Clean()
        {
            StopCoroutine(CloseRoutine());
            _previewImage.color = Color.white;
            _closeButton.onClick.RemoveAllListeners();
            _actionButton.onClick.RemoveAllListeners();
            _actionButton.gameObject.SetActive(false);
        }
    }
}
