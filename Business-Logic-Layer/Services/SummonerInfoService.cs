using Business_Logic_Layer.Interfaces;
using System.Text.Json;
using webapi.DTOs;

namespace Business_Logic_Layer.Services
{
    public sealed class SummonerInfoService : ISummonerInfoService
    {
        private readonly HttpClient _client;

        public SummonerInfoService(HttpClient client)
        {
               _client = client;
        }

        public async Task<SummonerDTO> GetSummonerInfoByNameAsync(string summonerName)
        {
            // Spaces in URL are expressed by "%20" 
            string SummonerNameAddedToURI = summonerName.Replace(" " , "%20");
            
            using HttpResponseMessage response = await _client.GetAsync($"{SummonerNameAddedToURI}");

            if(!response.IsSuccessStatusCode)
            { 
                throw new HttpRequestException("Summoner with given name does not exist", null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            SummonerDTO? summoner = JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);

            return summoner;
        }

        public async Task<string> GetSummonerPUUIDByNameAsync(string summonerName)
        {
            string SummonerNameAddedToURI = summonerName.Replace(" ", "%20");

            using HttpResponseMessage response = await _client.GetAsync($"{SummonerNameAddedToURI}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("An error occured but idk what happened XDD", null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            SummonerDTO? summoner = JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);

            return summoner.puuid;
        }
    }
}
