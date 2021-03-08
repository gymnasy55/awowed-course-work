using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JewelryStore.Desktop.Models
{
    //TODO: THIS SHIT
    //public struct _Settings
    //{
    //    [JsonProperty("work-price")]
    //    public float GramWorkPrice { get; set; }

    //    [JsonProperty("sale-price")]
    //    public float GramSalePrice { get; set; }
    //}

    //public static class Settings
    //{
    //    public static float GramWorkPrice { get; set; }

    //    public static float GramSalePrice { get; set; }

    //    static Settings()
    //    {
    //        var json = string.Empty;
    //        using (var fs = File.OpenRead("config.json"))
    //        {
    //            using var sr = new StreamReader(fs, new UTF8Encoding(false));
    //            json = sr.ReadToEnd();
    //        }

    //        var k = JsonConvert.DeserializeObject<_Settings>(json);

    //        GramWorkPrice = k.GramWorkPrice;
    //        GramSalePrice = k.GramSalePrice;
    //    }
    //}

    public static class Settings
    {
        public static float GramWorkPrice { get; set; } = 500;

        public static float GramSalePrice { get; set; } = 2200;
    }
}
