using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class OnBuyWidget : MonoBehaviour
    {
        public Image icon;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private CanvasGroup _canvasGroup;

        private float _lifeTime = 2.5f;
        private float _timer;

        private void FixedUpdate()
        {
            var pos = _rect.position;
            pos.y += 1 + Time.fixedDeltaTime;
            _rect.position = pos;

            _timer += Time.fixedDeltaTime;
            _canvasGroup.alpha -= .05f;
        
            if (_timer >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
