using System.Collections.Generic;

namespace Services
{
    public interface IPagesProvider
    {
        Dictionary<PagesEnum, IPageUI> PagesDictionary { get; }
        IConfirmWindow ConfirmWindow { get; }
        INotificationWindow NotificationWindow { get; }
        
        T GetPage<T>(PagesEnum type) where T : PageUI;
    }
}