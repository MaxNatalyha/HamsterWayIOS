using System.Collections.Generic;

namespace Pet
{
    public interface IPetFoodProvider
    {
        List<IPetFood> PetFood { get; }
        bool HasNonEmpty { get; }
        void SetFood(List<IPetFood> petFood);
        IPetFood[] GetNonEmptyFood();
        IPetFood GetMostSatietyFood();
        IPetFood GetFoodByType(PetFoodType type);
        IPetFood[] GetRandomFood();
    }
}