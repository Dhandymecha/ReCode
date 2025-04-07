using System.Text;
using System.Text.Json;

namespace Code
{
    public class Configration
    {
        public class ConfigJson
        {
            public string OPC_URL { get; set; } = "opc.tcp://192.168.250.1:4840";
            public int FILE_RETAIN_DAY { get; set; }
            public bool DB_USE { get; set; }
            public int DB_RETAIN_YEAR { get; set; }
        }

        public static ConfigJson Config { get; set; } = new ConfigJson();

        public static void Load()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var file = Path.Combine(directory, "conf.json");

            var json = File.ReadAllText(file, Encoding.UTF8);

            Config = JsonSerializer.Deserialize<ConfigJson>(json);
        }
        public static void Save(ConfigJson config)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var file = Path.Combine(directory, "conf.json");

            var json = JsonSerializer.Serialize<ConfigJson>(config);

            File.WriteAllText(file, json);
        }
    }
}
