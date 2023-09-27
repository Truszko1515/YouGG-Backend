using Business_Logic_Layer.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;
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
            _client = client;
            _summonerInfoService = summonerInfoService;
        }

        public  async Task<IEnumerable<string>> GetMatchListByNameAsync(string summonerName)
        {
            string summonerPUUID = await _summonerInfoService.GetSummonerPUUIDByNameAsync(summonerName);

            // base Uri - https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/

            using HttpResponseMessage response = await _client.GetAsync($"{summonerPUUID}/ids");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("An error occured but idk what happened XDD", null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<string>? matchesList = JsonSerializer.Deserialize<IEnumerable<string>>(jsonResponse);

            return matchesList;
        }

        // ostatni mecz id : "EUW1_6593708412"
    }
}
