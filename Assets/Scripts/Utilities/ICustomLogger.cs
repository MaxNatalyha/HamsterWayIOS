namespace Utilities
{
    public interface ICustomLogger
    {
        void PrintInfo(string name, string message);
        void PrintError(string name, string message);
    }
}