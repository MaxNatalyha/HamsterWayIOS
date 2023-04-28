namespace Pet
{
    public interface IPetFood
    {
        FoodState State { get; }
        FoodConfig Config { get; }
        void Load();
        void Save();
    }
}
