using Business_Logic_Layer.Interfaces;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;

        public MatchesService(HttpClient client, 
                              ISummonerInfoService summonerInfoService, 
                              IMemoryCache memoryCache)
        {
            _client = client;
            _summonerInfoService = summonerInfoService;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<string>> GetMatchListByPUUIDAsync(string summonerPUUID)
        {
            // Check if the match IDs are in the cache
            if (_memoryCache.TryGetValue($"Matches-{summonerPUUID}", out IEnumerable<string> cachedMatches))
            {
                return cachedMatches;
            }

            using HttpResponseMessage response = await _client.GetAsync($"{summonerPUUID}/ids?queue=420&start=0&count=20");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<string>? matchesList = JsonSerializer.Deserialize<IEnumerable<string>>(jsonResponse);

            // Cache the fetched match IDs with a relative expiration time
            if (matchesList != null)
            {
                _memoryCache.Set($"Matches-{summonerPUUID}", matchesList, TimeSpan.FromHours(1)); 
            }

            return matchesList;
        }
        
    }
}
