using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Repository;
using Data_Acces_Layer.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace webapi.Controllers
{
    [Route("api/[controller]/Insert/[action]/")]
    [EnableCors("LocalHostPolicy")]
    [ApiController]
    public class GlobalDataController : ControllerBase
    {
        private readonly IGlobalStatisticsRepository _globalStatisticsRepository;

        public GlobalDataController(IGlobalStatisticsRepository globalStatisticsRepository)
        {
            _globalStatisticsRepository = globalStatisticsRepository;
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult> ChampionData(string summonerName)
        {
            var insertSucceed = await _globalStatisticsRepository.UpdateChampionsData(summonerName);

            if (insertSucceed.result)
            {
                Console.Clear();
                Console.WriteLine($"Dodano do bazy {insertSucceed.championsInserted} meczy");
                return Ok($"Dodano do bazy {insertSucceed.championsInserted} meczy");
            }

            Console.Clear();
            Console.WriteLine("Dodawanie do bazy  statystyk globalnych Championów NIE POWIODŁO SIĘ");
            return BadRequest("Dodawanie do bazy  statystyk globalnych Championów NIE POWIODŁO SIĘ");
        }
    }
}
