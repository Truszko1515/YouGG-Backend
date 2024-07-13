using Business_Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using webapi.DTOs;

namespace Business_Logic_Layer.Services
{
    public sealed class SummonerPUUIDService : ISummonerPUUIDService
    {
        private readonly HttpClient _client;
        public SummonerPUUIDService(HttpClient client)
        {
            
                _client = client;
        }

        public async Task<string> GetSummonerPUUIDByNameAsync(string summonerName)
        {
            // Spaces in URL are expressed by "%20" 
            string Summoner = summonerName.Replace(" ", "%20");

            using HttpResponseMessage response = await _client.GetAsync($"{Summoner}/EUW");

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
