using System.Collections.Generic;

namespace Services
{
    public class PagesProvider : IPagesProvider
    {
        public Dictionary<PagesEnum, IPageUI> PagesDictionary { get; }
        public IConfirmWindow ConfirmWindow { get; }
        public INotificationWindow NotificationWindow { get; }

        public PagesProvider(PageUI[] pages, IConfirmWindow confirmWindow, INotificationWindow notificationWindow)
        {
            PagesDictionary = new Dictionary<PagesEnum, IPageUI>(pages.Length);
            
            foreach (var page in pages)
                PagesDictionary.Add(page.CurrentPage, page);

            ConfirmWindow = confirmWindow;
            NotificationWindow = notificationWindow;
        }
        
        public T GetPage<T>(PagesEnum type) where T : PageUI
        {
            return PagesDictionary[type] as T;
        }
    }
}