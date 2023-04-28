using System;

namespace Services
{
    public interface IConfirmWindow
    {
        void Open(Action onYes, Action onNo);
        void Open(Action onYes);
    }
}
