using System.Collections.Generic;
using UnityEngine;

namespace Pet
{
    [CreateAssetMenu(menuName = "Pet/PetConfigurations")]
    public class PetConfig : ScriptableObject
    {
        public int minSatiety;
        public int maxSatiety;
        public int satietySpendPerTick;
        public float tickInSeconds;
        
        public List<PetState> petStates;
    }
}