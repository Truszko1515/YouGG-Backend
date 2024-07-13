using Business_Logic_Layer.Interfaces;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text.Json;
using webapi.DTOs;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Diagnostics;

namespace Business_Logic_Layer.Services
{
    public sealed class MatchDetailsService : IMatchDetailsService
    {
        private readonly HttpClient _client;

        public MatchDetailsService(HttpClient client) 
        {
            _client = client;
        }

        public async Task<MatchDto> GetMatchDetailsByMatchIdAsync(string matchId)
        {
            using  HttpResponseMessage response = await _client.GetAsync($"{matchId}");
            
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;
                
                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            MatchDto? matchDetails = JsonSerializer.Deserialize<MatchDto>(jsonResponse);

            return matchDetails;
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
