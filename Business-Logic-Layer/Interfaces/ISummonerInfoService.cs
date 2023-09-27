using webapi.DTOs;

namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerInfoService
    {
        Task<SummonerDTO> GetSummonerInfoByNameAsync(string SummonerName);
        Task<string> GetSummonerPUUIDByNameAsync(string summonerName);
    }
}
