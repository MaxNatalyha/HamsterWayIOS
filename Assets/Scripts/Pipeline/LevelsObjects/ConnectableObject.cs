using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pipeline
{
    [RequireComponent(typeof(RectTransform))]
    public class ConnectableObject : MonoBehaviour, IConnectable
    {
        public ObjectConfig ObjectConfig => _objectConfig;

        [Header("Connectable object")]
        [SerializeField] protected List<Connector> _connectors;
        [SerializeField] protected ObjectConfig _objectConfig;
        [SerializeField] protected Image _viewImage;
        [SerializeField] protected RectTransform _mainContainer;
        [SerializeField] protected RectTransform _contentContainer;
        [SerializeField] protected RectTransform _defaultViewParent;

        [Space(10)]
        [Header("Debug")]
        [SerializeField] private TMP_Text _debugText;
        [SerializeField] private Image _debugImage;

        private RectTransform _upperViewParent;

        public void ToggleViewParent()
        {
            _viewImage.rectTransform.SetParent(_viewImage.rectTransform.parent == _upperViewParent ? _defaultViewParent : _upperViewParent);
        }

        public void ReturnToPool(RectTransform pool)
        {
            transform.SetParent(pool);
            gameObject.SetActive(false);
        }

        public virtual void SetUp(Vector2 size, Vector2 position, RectTransform upperViewParent, RectTransform mainParent)
        {
            transform.SetParent(mainParent);

            _mainContainer.sizeDelta = size;
            _mainContainer.anchoredPosition = position;
            
            _upperViewParent = upperViewParent;

            gameObject.SetActive(true);
        }

        public void SetDebugMode(bool value)
        {
            _debugImage.enabled = value;
            _debugText.enabled = value;
        }

        public void UpdateDebugUI()
        {
            int allConnector = _connectors.Count;
            int activeConnectors = _connectors.FindAll(c => c.IsConnected).Count;
            
            _debugText.text = $"{allConnector}:{activeConnectors}";
            var newColor = Color.Lerp(new Color(1,0,0,.33f), new Color(0,1,0,.33f), (float)activeConnectors / (float)allConnector);

            _debugImage.color = newColor;
        }
    }
}
