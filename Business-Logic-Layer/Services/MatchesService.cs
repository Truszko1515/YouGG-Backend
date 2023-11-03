using Business_Logic_Layer.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;
using webapi.DTOs;

namespace Business_Logic_Layer.Services
{
    public sealed class MatchesService : IMatchesService
    {
        private readonly HttpClient _client;
        private readonly ISummonerInfoService _summonerInfoService;

        public MatchesService(HttpClient client, ISummonerInfoService summonerInfoService)
        {
            // base request Uri - https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/
            _client = client;
            _summonerInfoService = summonerInfoService;
        }

        public async Task<IEnumerable<string>> GetMatchListByNameAsync(string summonerName)
        {
            string summonerPUUID = await _summonerInfoService.GetSummonerPUUIDByNameAsync(summonerName);

            using HttpResponseMessage response = 
                await _client.GetAsync($"{summonerPUUID}/ids?queue=420&start=0&count=20");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<string>? matchesList = JsonSerializer.Deserialize<IEnumerable<string>>(jsonResponse);

            return matchesList;
        }

        public async Task<IEnumerable<string>> GetMatchListByPUUIDAsync(string summonerPUUID)
        {
            using HttpResponseMessage response = await _client.GetAsync($"{summonerPUUID}/ids");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<string>? matchesList = JsonSerializer.Deserialize<IEnumerable<string>>(jsonResponse);

            return matchesList;
        }
    }
}
