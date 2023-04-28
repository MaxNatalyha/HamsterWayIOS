using System.Collections.Generic;
using UnityEngine;

namespace Pet
{
    [CreateAssetMenu(menuName = "Pet/PetFoodConfigurations")]
    public class PetFoodConfigurations : ScriptableObject
    {
        public List<FoodConfig> petFoodConfigs;
    }
}
