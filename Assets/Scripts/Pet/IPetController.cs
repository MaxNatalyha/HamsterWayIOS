using System;
using Cysharp.Threading.Tasks;
using GameUI;
using Services;

namespace Pet
{
    public interface IPetController
    {
        event Action<float> OnSatietyLevelChangeEvent;
        float Satiety { get; }
        UniTask Load();
        void Initialize();
        void Save();
    }
}