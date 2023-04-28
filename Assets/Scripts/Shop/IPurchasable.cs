using Services;

namespace Shop
{
    public interface IPurchasable
    {
        CurrencyType Currency { get; }
        int Price { get; }
    }
}