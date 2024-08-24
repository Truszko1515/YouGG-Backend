using Business_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IMemoryCache _memoryCache;

        public SummonerPUUIDService(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
        }

        public async Task<string> GetSummonerPUUIDByNameAsync(string SummonerName)
        {
            // Normalize SummonerName to use as a cache key
            string normalizedSummonerName = SummonerName.ToLowerInvariant();

            // Check if the PUUID is in the cache
            if (_memoryCache.TryGetValue(normalizedSummonerName, out string cachedPuuid))
            {
                return cachedPuuid;
            }

            string tagLine = ExtractTagline(SummonerName);
            string gameName = string.Empty;

            // If user appended tagline, and it's not #EUW
            if(!string.IsNullOrEmpty(tagLine))
            {
                gameName = SummonerName.Substring(0, SummonerName.Length - tagLine.Length - 1);
                gameName = gameName.Replace(" ", "");
            }
            else
            {
                tagLine = "EUW";
                gameName = SummonerName.Replace("%", " ");
            }


            using HttpResponseMessage response = await _client.GetAsync($"{gameName}/{tagLine}");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            SummonerDTO? summoner = JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);

            if (summoner != null)
            {
                // Cache the PUUID with a relative expiration time
                _memoryCache.Set(normalizedSummonerName, summoner.puuid, TimeSpan.FromHours(1));
                return summoner.puuid;
            }

            return string.Empty;
        }

        private static string ExtractTagline(string input)
        {
            // Check if the string contains a '#' character
            int taglineIndex = input.IndexOf('#');

            // If a '#' is found and it is not at the start of the string
            if (taglineIndex != -1 && taglineIndex != 0)
            {
                // Extract the tagline starting from the '#' character to the end of the string
                string tagline = input.Substring(taglineIndex+1);
                return tagline;
            }

            // Return false if no tagline is found
            return string.Empty;
        }
    }
}
