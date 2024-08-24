using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Helpers
{
    public static class ChampionMappingHelper
    {
        public static async Task<Dictionary<long, string>> GetChampionsAsync()
        {
            using var client = new HttpClient();

            var url = "https://ddragon.leagueoflegends.com/cdn/14.16.1/data/en_US/champion.json"; // Upewnij się, że używasz aktualnej wersji API

            var response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<ChampionData>(response);

            return data.Data.ToDictionary(champion => long.Parse(champion.Value.Key), champion => champion.Value.Id);
        }
    }


    public class ChampionData
    {
        [JsonProperty("data")]
        public Dictionary<string, Champion> Data { get; set; }
    }

    public class Champion
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Id { get; set; }
    }
}
