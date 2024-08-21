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
    public class SummonerTagLineService : ISummonerTagLineService
    {
        private readonly HttpClient _client;
        public SummonerTagLineService(HttpClient client)
        {
            _client = client;
        }
        public async Task<string> GetSummonerTagLine(string SummonerPUUID)
        {

            using HttpResponseMessage response = await _client.GetAsync($"{SummonerPUUID}");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessageDto>();
                string message = responseMessage.status.message;

                throw new HttpRequestException(message, null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            AccountDto? summonerAccount = JsonSerializer.Deserialize<AccountDto>(jsonResponse);

            if (summonerAccount == null)
            {
                string message = "Problem occured during fetching this account";

                throw new HttpRequestException(message, null, HttpStatusCode.BadRequest);
            }

            return summonerAccount.tagLine;
        }
    }
}
