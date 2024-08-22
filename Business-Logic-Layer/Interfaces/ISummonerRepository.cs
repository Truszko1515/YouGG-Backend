using Business_Logic_Layer.Dtos;

namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerRepository
    {
        Task<double> GetSummonerKDA(string summonerName);
        Task<IEnumerable<ChampionsPlayRateDto>> GetSummonerChampionsPlayRate(string summonerName);
        Task<double> GetKillParticipation(string summonerName);
        Task<IEnumerable<PositionsChartDto>> GetPosition(string summonerName);
        Task<LastGamesWinRateDto> GetLastGamesWinRate(string summonerName);
    }
}
