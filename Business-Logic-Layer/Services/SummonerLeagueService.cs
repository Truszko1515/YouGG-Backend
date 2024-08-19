using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
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
        public SummonerLeagueService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<LeagueEntryDto>> GetLeagueEntry(string SummonerId)
        {
            using HttpResponseMessage response = await _client.GetAsync($"{SummonerId}");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var SummonerLeagueEntry = JsonSerializer.Deserialize<List<LeagueEntryDto>>(jsonResponse);

            var result = SummonerLeagueEntry.Select(x => x).Where(x => x.queueType == "RANKED_SOLO_5x5").ToList();

            if(SummonerLeagueEntry == null)
            {
                string message = "This summoner does not have any league entries.";

                throw new HttpRequestException(message, null, HttpStatusCode.BadRequest);
            }

            return result!;
        }
    }
}
