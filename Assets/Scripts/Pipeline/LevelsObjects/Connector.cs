using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pipeline
{
    public class Connector : MonoBehaviour
    {
        public event Action<bool> OnConnectionChangeEvent;
        public RectTransform Rect { get; private set; }
        public Connector NextObjectConnector
        {
            get => _nextObjectConnector;
            set
            {
                _nextObjectConnector = value;
                ConnectionChange();
            }
        }
        
        public ConnectableObject ParentConnectableObject { get; private set; }
        public bool IsConnected => _nextObjectConnector != null;
        public RectTransform MidPathPoint => _midPathPoint;
        public Connector Output => _output;

        [SerializeField] private RectTransform _midPathPoint;
        [SerializeField] private Connector _output;
            
        private Connector _nextObjectConnector;
        private Image _debugImage;
        private bool _debugMode = false;

        private void Awake()
        {
            _debugImage = GetComponent<Image>();
            Rect = GetComponent<RectTransform>();
            ParentConnectableObject = GetComponentInParent<ConnectableObject>();
        
            SetDebugMode(_debugMode);
        }

        private void SetDebugMode(bool value)
        {
            _debugImage.enabled = value;
            ParentConnectableObject.SetDebugMode(value);
        }

        private void ConnectionChange()
        {
            OnConnectionChangeEvent?.Invoke(IsConnected);

            if (!_debugMode) return;
            
            _debugImage.color = IsConnected ? Color.cyan : Color.gray;
            ParentConnectableObject.UpdateDebugUI();
        }
    
        private void OnTriggerStay2D(Collider2D other)
        {
            if(IsConnected) return;
        
            if (other.GetComponentInParent<Connector>())
            {
                var connector = other.GetComponent<Connector>();

                if (NextObjectConnector != connector)
                    NextObjectConnector = connector;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponentInParent<Connector>())
            {
                NextObjectConnector = null;
            }
        }

        private void OnDisable()
        {
            _nextObjectConnector = null;
        }
    }
}
