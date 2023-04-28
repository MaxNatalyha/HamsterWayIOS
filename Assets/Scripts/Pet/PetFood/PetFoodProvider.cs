using System;
using System.Collections.Generic;
using System.Linq;

namespace Pet
{
    public class PetFoodProvider : IPetFoodProvider
    {
        public List<IPetFood> PetFood { get; private set; }
        public bool HasNonEmpty => PetFood.Exists(f => !f.State.IsEmpty);
        
        public void SetFood(List<IPetFood> petFood)
        {
            PetFood = petFood;
        }

        public IPetFood[] GetNonEmptyFood()
        {
            return PetFood.FindAll(f => !f.State.IsEmpty).ToArray();
        }       
        
        public IPetFood GetMostSatietyFood()
        {
            return PetFood.FindAll(f => !f.State.IsEmpty).OrderByDescending(f=>f.Config.Satiety).First();
        }

        public IPetFood GetFoodByType(PetFoodType type)
        {
            return PetFood.Find(f => f.Config.FoodType == type);
        }
        
        public IPetFood[] GetRandomFood()
        {
            var rng = new Random();
            var randomFood = PetFood.OrderBy(f => rng.Next()).Take(3).ToArray();

            return randomFood;
        }
    }
}