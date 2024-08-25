using Business_Logic_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Helpers
{
    public static class SummonerSpellHelper
    {
        public static string GetSummonerSpell(int key)
        {
            var type = typeof(SpellImagesEnum);
            var name = Enum.GetName(type, key);
            if (name == null) return "Key not found"; 

            var memberInfo = type.GetMember(name);
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }

            return "Description not found for given key";
        }
    }


    public class SummonerSpellData
    {
        [JsonPropertyName("data")]
        public Dictionary<string, SummonerSpell> Data { get; set; }
    }

    public class SummonerSpell
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("image")]
        public SummonerSpellImage Image { get; set; }
    }

    public class SummonerSpellImage
    {
        [JsonPropertyName("full")]
        public string Full { get; set; }
    }
}
