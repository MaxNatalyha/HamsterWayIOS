using System.IO;
using Newtonsoft.Json;

namespace SaveSystem
{
    public class JsonSaveSystem : ISaveSystem
    {
        private readonly string _saveDirectory;

        public JsonSaveSystem(string saveDirectory)
        {
            _saveDirectory = saveDirectory;
        }
    
        public void Save<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        
            using (var writer = new StreamWriter(_saveDirectory))
            {
                writer.WriteLine(json);
            }
        }
    
        public T Load<T>()
        {
            if (!File.Exists(_saveDirectory))
                return default;
        
            string json = "";

            using (var reader = new StreamReader(_saveDirectory))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    json += line;
                }
            }
        
            if(string.IsNullOrEmpty(json))
                return default;
        
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
