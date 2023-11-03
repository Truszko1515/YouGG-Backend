using Business_Logic_Layer.Interfaces;
using System.Net.Http.Json;
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
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            SummonerDTO? summoner = JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);

            return summoner;
        }

        public async Task<string> GetSummonerPUUIDByNameAsync(string summonerName)
        {
            
            using HttpResponseMessage response = await _client.GetAsync($"{summonerName.Replace(" ", "%20")}");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            SummonerDTO? summoner = JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);

            return summoner.puuid;
        }
    }
}
