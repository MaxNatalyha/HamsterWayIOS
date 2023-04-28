using Utilities;

namespace Pet
{ 
    public class PetFood : IPetFood
    {
        public FoodState State { get; }
        public FoodConfig Config { get; }
        
        private readonly ISaver _saver;
        private readonly ICustomLogger _logger;

        public PetFood(FoodConfig config)
        {
            Config = config;
            State = new FoodState();
            _saver = new PrefsSaver();
            _logger = new DebugLogger();
        }
    
        public void Save()
        {
            _saver.SetInt(Config.FoodType.ToString(), State.Amount);
            //_logger.PrintInfo(Config.FoodType.ToString(), $"Save - {State.Amount}");
        }

        public void Load()
        {
            var key = Config.FoodType.ToString();
            State.Amount = _saver.HasKey(key) ? _saver.GetInt(key) : 0;
            //_logger.PrintInfo(Config.FoodType.ToString(), $"Load - {State.Amount}");
        }
    }
}
