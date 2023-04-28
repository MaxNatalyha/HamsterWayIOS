using UnityEngine;

namespace Services
{
    public class PageUI : MonoBehaviour, IPageUI
    {
        public PagesEnum CurrentPage => _page;
    
        [Header("Page UI")]
        [SerializeField] private PagesEnum _page;
    
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
