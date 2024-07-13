using webapi.DTOs;

namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerInfoService
    {
        Task<SummonerDTO> GetSummonerInfoByPuuidAsync(string summonerPUUID);
    }
}
