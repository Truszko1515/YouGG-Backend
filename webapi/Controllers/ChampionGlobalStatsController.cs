using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Interfaces.GlobalStats;
using Business_Logic_Layer.Services.GlobalStatsServices;
using Data_Acces_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChampionGlobalStatsController : ControllerBase
    {
        private readonly IChampionsStatisticService _championsStatisticService;

        public ChampionGlobalStatsController(IChampionsStatisticService championsStatisticService)
        {
            _championsStatisticService = championsStatisticService;
        }

        [HttpPost("ChampionStatistics")]
        public async Task<IActionResult> GetChampionStatistics([FromBody] ChampionStatisticsRequestDto request)
        {
            // Sprawdzenie poprawności requestu
            if (string.IsNullOrEmpty(request.ChampionName) || request.SelectedColumns == null)
            {
                return BadRequest("Invalid request.");
            }

            var stats = await _championsStatisticService.GetChampionStatisticsAsync(request);

            if (stats == null)
            {
                return NotFound("No statistics found for the given criteria.");
            }

            return Ok(stats);
        }
    }
}



// Zastosowanie filtrów
/*if (!string.IsNullOrEmpty(filters.Lane) && filters.Lane != "ALL")
{
    query = query.Where(ms => ms.TeamPosition == filters.Lane);
}

if (!string.IsNullOrEmpty(filters.Result))
{
    query = query.Where(ms => ms.Win == (filters.Result == "Win"));
}

if (!string.IsNullOrEmpty(filters.Kills.Value))
{
    var killsValue = int.Parse(filters.Kills.Value);
    if (filters.Kills.Comparison == "greater")
        query = query.Where(ms => ms.Kills > killsValue);
    else
        query = query.Where(ms => ms.Kills < killsValue);
}

if (!string.IsNullOrEmpty(filters.TotalDamageDealt.Value))
{
    var damageValue = int.Parse(filters.TotalDamageDealt.Value);
    if (filters.TotalDamageDealt.Comparison == "greater")
        query = query.Where(ms => ms.TotalDamageDealtToChampions > damageValue);
    else
        query = query.Where(ms => ms.TotalDamageDealtToChampions < damageValue);
}

// Zastosowanie innych filtrów w podobny sposób
if (!string.IsNullOrEmpty(filters.GameLength.Value))
{
    var gameLengthValue = int.Parse(filters.GameLength.Value) * 60; // Game length in seconds
    if (filters.GameLength.Comparison == "greater")
        query = query.Where(ms => ms.GameLengthSeconds > gameLengthValue);
    else
        query = query.Where(ms => ms.GameLengthSeconds < gameLengthValue);
}

// Zwracanie dynamicznych kolumn
var results = await query.Select(ms => new
{
    ChampionName = ms.ChampionName,
    // Dynamically select columns based on selectedColumns list
    WinRatio = selectedColumns.Contains("winRatio") ? ms.Win : (bool?)null,
    MinionsFirst10Minutes = selectedColumns.Contains("minionsFirst10Minutes") ? ms.MinionsFirst10Minutes : (int?)null,
    TotalCS = selectedColumns.Contains("totalCS") ? ms.TotalCS : (int?)null,
    CSperMinute = selectedColumns.Contains("csPerMinute") ? ms.CSperMinute : (float?)null,
    KDA = selectedColumns.Contains("kda") ? (ms.Kills + ms.Assists) / (double)(ms.Deaths == 0 ? 1 : ms.Deaths) : (double?)null,
    DmgDealt = selectedColumns.Contains("dmgDealt") ? ms.TotalDamageDealtToChampions : (int?)null,
    VisionScore = selectedColumns.Contains("visionScore") ? ms.VisionScore : (int?)null,
    Kills = selectedColumns.Contains("kills") ? ms.Kills : (int?)null,
    Deaths = selectedColumns.Contains("deaths") ? ms.Deaths : (int?)null,
    Assists = selectedColumns.Contains("assists") ? ms.Assists : (int?)null,
    TotalDamageDealtToChampions = selectedColumns.Contains("totalDamageDealtToChampions") ? ms.TotalDamageDealtToChampions : (int?)null,
    GameLength = selectedColumns.Contains("gameLength") ? ms.GameLengthSeconds : (int?)null
}).ToListAsync();*/
