using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using webapi.DTOs;

namespace Business_Logic_Layer.Services
{
    public sealed class SummonerInfoService
    {
        private readonly HttpClient _client;

        public SummonerInfoService(HttpClient client)
        {
               _client = client;
        }

        public async Task<SummonerDTO> GetSummonerInfoByNameAsync(string SummonerName, string ApiKey)
        {
            // Spaces in URL are expressed by "%20" 
            string SummonerNameAddedToURI = SummonerName.Replace(" ", "%20");

            var content = await _client.GetFromJsonAsync<SummonerDTO>($"{SummonerNameAddedToURI}?api_key={ApiKey}");

            return content;
        }
    }
}
