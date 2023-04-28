using Cysharp.Threading.Tasks;

namespace GameUI
{
    public interface IUIBuilder
    {
        UniTask Load();
        void Build();
    }
}