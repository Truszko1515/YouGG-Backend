using Business_Logic_Layer.Interfaces;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text.Json;
using webapi.DTOs;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Business_Logic_Layer.Services
{
    public sealed class MatchDetailsService : IMatchDetailsService
    {
        private readonly HttpClient _client;
        private readonly IMatchesService _matchesService;
        public MatchDetailsService(HttpClient client, IMatchesService matchesService) 
        {
            // base request Uri - https://europe.api.riotgames.com/lol/match/v5/matches/
            _client = client;
            _matchesService = matchesService;
        }

        public async Task<MatchDto> GetLastMatchDetailsByNameAsync(string summonerName)
        {
            IEnumerable<string> summonerMatchIDs = await _matchesService.GetMatchListByNameAsync(summonerName);

            string? summonerLastMatchID = summonerMatchIDs.FirstOrDefault();

            using HttpResponseMessage response = await _client.GetAsync($"{summonerLastMatchID}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Coś się kurcze stało przy pobieraniu szczegołów meczu", null, response.StatusCode);
            }
             
            var jsonResponse = await response.Content.ReadAsStringAsync();
            MatchDto? matchDetails = JsonSerializer.Deserialize<MatchDto>(jsonResponse);

            return matchDetails;
        }

        public async Task<MatchDto> GetMatchDetailsByMatchIdAsync(string matchId)
        {
            using HttpResponseMessage response = await _client.GetAsync($"{matchId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Coś się kurcze stało przy pobieraniu szczegołów meczu", null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            MatchDto? matchDetails = JsonSerializer.Deserialize<MatchDto>(jsonResponse);

            return matchDetails;
        }

        public async Task<IEnumerable<string>> GetParticipantsOfLastMatchAsync(string summonerName)
        {
            MatchDto? match = await GetLastMatchDetailsByNameAsync(summonerName);

            return match.info.participants.Select(participants => participants.summonerName);
        }

        public async Task<double> GetSummonerKDAFromLastTwentyGames(string summonerName)
        {
            var matchesList = await _matchesService.GetMatchListByNameAsync(summonerName);

            IEnumerable<MatchDto> matches = await GetMatchDetailsListByMatchIdsAsync(matchesList);


            var summonerStats = matches.SelectMany(match => match.info.participants).
                                Where(participant => participant.summonerName == summonerName);

            var kills = summonerStats.Select(s => s.kills);
                                


            return kills.Average();
        }

        public async Task<List<MatchDto>> GetMatchDetailsListByMatchIdsAsync(IEnumerable<string> matchIdsList)
        {
            List<MatchDto> matchDetailsList = new List<MatchDto>();
            
            foreach (var matchId in matchIdsList) 
            { 
                MatchDto matchDetails = await GetMatchDetailsByMatchIdAsync(matchId);
                
                matchDetailsList.Add(matchDetails);
            }

            return matchDetailsList;
        }

    }
}
