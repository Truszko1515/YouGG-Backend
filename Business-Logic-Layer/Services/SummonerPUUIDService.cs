using Business_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public SummonerPUUIDService(HttpClient client)
        {    
                _client = client;
        }

        public async Task<string> GetSummonerPUUIDByNameAsync(string SummonerName)
        {
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

            return summoner.puuid;
        }

        public static string ExtractTagline(string input)
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
