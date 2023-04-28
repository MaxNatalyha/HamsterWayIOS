using System;
using UnityEngine;

namespace Pet
{
    [Serializable]
    public class PetState
    {
        public PetStateEnum state;
        [Range(0, 100)]
        public int satietyEnter;
        [Range(0, 100)]
        public int satietyExit;
    }
}