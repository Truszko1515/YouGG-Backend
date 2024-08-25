using Business_Logic_Layer.Dtos;

namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerRepository
    {
        Task<double> GetKDA(string summonerName);
        Task<IEnumerable<SummonerChampionsPlayRateDto>> GetChampionsPlayRate(string summonerName);
        Task<double> GetKillParticipation(string summonerName);
        Task<IEnumerable<SummonerPositionsChartDto>> GetPosition(string summonerName);
        Task<SummonerLastGamesDto> GetLastGamesWinRate(string summonerName);
        Task<SummonerLeagueEntryDto> GetLeagueEntries(string summonerName);
        Task<List<SummonerMasteryDto>> GetChampionsMastery(string summonerName);
        Task<List<SummonerMatchDetailsDto>> GetMatchesDetails(string summonerName);

    }
}
