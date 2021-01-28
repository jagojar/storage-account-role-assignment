using System;
using System.IO;
using System.Text.Json;

namespace StorageAccountApp
{
    public class ConfigManager
    {
        public ConfigBody GetConfig()
        {
            string path = @"C:\MS\Clients\Pilot-Catastrophe\config-az-account.json";
            string fileContent = File.ReadAllText(path);
            var configBody = JsonSerializer.Deserialize<ConfigBody>(fileContent);

            return configBody;
        }
    }
}