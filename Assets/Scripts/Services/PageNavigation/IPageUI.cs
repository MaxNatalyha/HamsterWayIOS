namespace Services
{
    public interface IPageUI
    {
        PagesEnum CurrentPage { get; }
        void Open();
        void Close();
    }
}
