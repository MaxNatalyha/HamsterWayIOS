using System;
using Cysharp.Threading.Tasks;
using Shop;
using UnityEngine.Purchasing;

namespace Services
{
    public interface IIAPService
    {
        event Action<string> OnProcessPurchaseEvent;
        void Initialize();
        Product GetProduct(string productID);
        void Restore();
    }
}