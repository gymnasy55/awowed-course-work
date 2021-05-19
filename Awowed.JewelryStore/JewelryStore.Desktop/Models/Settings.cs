using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JewelryStore.Desktop.Models
{
    public static class Settings
    {
        private struct JsonSettingsStruct
        {
            [JsonProperty("work-price")]
            public float GramWorkPriceJson { get; set; }

            [JsonProperty("sale-price")]
            public float GramSalePriceJson { get; set; }
        }

        public static float GramWorkPrice { get; set; }

        public static float GramSalePrice { get; set; }

        static Settings()
        {
            ReadConfig();
        }

        public static void ReadConfig()
        {
            string json;
            using (var fs = File.OpenRead("config.json"))
            {
                using var sr = new StreamReader(fs, new UTF8Encoding(false));
                json = sr.ReadToEnd();
            }

            var jsonStruct = JsonConvert.DeserializeObject<JsonSettingsStruct>(json);

            GramWorkPrice = jsonStruct.GramWorkPriceJson;
            GramSalePrice = jsonStruct.GramSalePriceJson;
        }

        public static void WriteConfig()
        {
            var jsonStruct = new JsonSettingsStruct
            {
                GramWorkPriceJson = GramWorkPrice, GramSalePriceJson = GramSalePrice
            };

            var jsonString = JsonConvert.SerializeObject(jsonStruct);

            using var sw = new StreamWriter("config.json", false);
            sw.Write(jsonString);
        }
    }
}
