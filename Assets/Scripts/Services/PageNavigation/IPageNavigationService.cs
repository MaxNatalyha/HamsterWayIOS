namespace Services
{
    public interface IPageNavigationService
    {
        IConfirmWindow ConfirmWindow { get; }
        INotificationWindow NotificationWindow { get; }
        IPageNavigationService OpenPage(PagesEnum pageToOpen);
        IPageNavigationService OpenPage(GamesTypes selectionPageType);
        IPageNavigationService ClosePage(PagesEnum pageToClose);
        IPageNavigationService ClosePage(GamesTypes selectionPageType);
    }
}