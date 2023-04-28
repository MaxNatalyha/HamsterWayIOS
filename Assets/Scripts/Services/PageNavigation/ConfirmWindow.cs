using System;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class ConfirmWindow : MonoBehaviour, IConfirmWindow
    {
        [SerializeField] private Button _yesButton, _noButton;
        
        public void Open(Action onYes, Action onNo)
        {
            Open(onYes);
            _yesButton.onClick.AddListener(() => onNo?.Invoke());
        }

        public void Open(Action onYes)
        {
            _yesButton.onClick.AddListener(() => onYes?.Invoke());
            
            gameObject.SetActive(true);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _yesButton.onClick.AddListener(Close);
            _noButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _yesButton.onClick.RemoveAllListeners();
            _noButton.onClick.RemoveAllListeners();
        }
    }
}
