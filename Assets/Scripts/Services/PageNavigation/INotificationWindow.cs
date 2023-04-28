using System;
using Shop.SkinsSection;
using UnityEngine;

namespace Services
{
    public interface INotificationWindow
    {
        void Open(string content, Sprite icon);
        void Open(string content, Sprite icon, Action action);
    }
}
