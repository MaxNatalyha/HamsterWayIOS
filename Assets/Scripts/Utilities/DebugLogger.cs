using UnityEngine;

namespace Utilities
{
    public class DebugLogger : ICustomLogger
    {
        public void PrintInfo(string name, string message)
        {
            Debug.Log($"<b>{name}</b>: {message}");
        }

        public void PrintError(string name, string message)
        {
            Debug.LogError($"<b>{name}</b>: {message}");
        }
    }
}