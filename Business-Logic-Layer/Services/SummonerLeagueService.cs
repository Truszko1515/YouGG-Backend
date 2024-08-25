using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using webapi.DTOs;

namespace Business_Logic_Layer.Services
{
    public sealed class SummonerLeagueService : ISummonerLeagueService
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _memoryCache;

        public SummonerLeagueService(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
        }

        public async Task<SummonerLeagueEntryDto> GetLeagueEntry(string SummonerId)
        {
            string cacheKey = $"LeagueEntry-{SummonerId}";

            if (_memoryCache.TryGetValue(cacheKey, out SummonerLeagueEntryDto cachedLeagueEntry))
            {
                return cachedLeagueEntry;
            }

            using HttpResponseMessage response = await _client.GetAsync($"{SummonerId}");
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;
                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var SummonerLeagueEntry = JsonSerializer.Deserialize<List<SummonerLeagueEntryDto>>(jsonResponse);

            var finalResult = SummonerLeagueEntry.FirstOrDefault(x => x.queueType == "RANKED_SOLO_5x5");

            if (finalResult == null)
            {
                string message = "This summoner does not have any league entries.";
                throw new HttpRequestException(message, null, HttpStatusCode.BadRequest);
            }

            // Cache the fetched league entry
            _memoryCache.Set(cacheKey, finalResult, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1)));

            return finalResult;
        }
    }
}
