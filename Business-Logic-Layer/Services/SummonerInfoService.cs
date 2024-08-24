using Business_Logic_Layer.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using System.Text.Json;
using webapi.DTOs;

namespace Business_Logic_Layer.Services
{
    public sealed class SummonerInfoService : ISummonerInfoService
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _memoryCache;

        public SummonerInfoService(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
        }

        public async Task<SummonerDTO> GetSummonerInfoByPuuidAsync(string summonerPUUID)
        {
            string cacheKey = $"SummonerInfo-{summonerPUUID}";

            // Try to retrieve the summoner info from cache
            if (_memoryCache.TryGetValue(cacheKey, out SummonerDTO cachedSummoner))
            {
                return cachedSummoner;
            }

            using HttpResponseMessage response = await _client.GetAsync($"{summonerPUUID}");
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;
                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            SummonerDTO? summoner = JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);

            // Cache the fetched summoner info
            if (summoner != null)
            {
                _memoryCache.Set(cacheKey, summoner, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1)));  // Customize the expiration as needed
            }

            return summoner ?? throw new InvalidOperationException("Failed to deserialize the summoner data.");
        }

    }
}
