namespace SaveSystem
{
    public interface IDataLoader<TData>
    {
        TData Load();
        void Save(TData saveData);
    }
}
