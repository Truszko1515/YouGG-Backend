using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Business_Logic_Layer.Services
{
    public class SummonerMasteryService : ISummonerMasteryService
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _memoryCache;

        public SummonerMasteryService(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<SummonerMasteryDto>> GetSummonerChampionsMastery(string SummonerPUUID)
        {
            string cacheKey = $"ChampionsMastery-{SummonerPUUID}";

            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<SummonerMasteryDto> cachedChampionsMastery))
            {
                return cachedChampionsMastery;
            }

            using HttpResponseMessage response = await _client.GetAsync($"{SummonerPUUID}/top?count=7");
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;
                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var SummonerChampionsMastery = JsonSerializer.Deserialize<List<SummonerMasteryDto>>(jsonResponse);

            if (SummonerChampionsMastery == null)
            {
                string message = "Error during fetching summoner champions mastery";
                throw new HttpRequestException(message, null, HttpStatusCode.BadRequest);
            }

            // Cache the fetched league entry
            _memoryCache.Set(cacheKey, SummonerChampionsMastery, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1)));

            return SummonerChampionsMastery;
        }
    }
}
