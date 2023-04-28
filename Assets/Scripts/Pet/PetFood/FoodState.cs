using System;

namespace Pet
{
    public class FoodState
    {
        public event Action<int> OnAmountChangeEvent;
        public int Amount         
        {
            get => _amount;
            set
            {
                _amount = value;
                
                if (_amount < 0)
                    _amount = 0;
            
                OnAmountChangeEvent?.Invoke(Amount);
            }
        }
    
        public bool IsEmpty => Amount <= 0;
        public bool IsLast => Amount == 1;
        
        private int _amount;
    }
}
