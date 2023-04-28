using System.Collections.Generic;

namespace Pet
{
    public class PetFoodFactory
    {
        private readonly PetFoodConfigurations _petFoodConfigurations;
        
        public PetFoodFactory(PetFoodConfigurations petFoodConfigurations)
        {
            _petFoodConfigurations = petFoodConfigurations;
        }
        
        public List<IPetFood> CreatePetFood()
        {
            var createdPetFood = new List<IPetFood>();
            
            foreach (var config in _petFoodConfigurations.petFoodConfigs)
            {
                var food = new PetFood(config);
                food.Load();
                createdPetFood.Add(food);
            }

            return createdPetFood;
        }
    }
}