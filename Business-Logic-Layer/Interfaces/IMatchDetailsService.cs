namespace Business_Logic_Layer.Interfaces
{
    public interface IMatchDetailsService
    {
         Task<MatchDto> GetLastMatchDetailsByNameAsync(string summonerName);
         Task<MatchDto> GetMatchDetailsByMatchIdAsync(string matchId);
         Task<IEnumerable<string>> GetParticipantsOfLastMatchAsync(string summonerName);
         Task<double> GetSummonerKDAFromLastTwentyGames(string summonerName);
         Task<List<MatchDto>> GetMatchDetailsListByMatchIdsAsync(IEnumerable<string> matchIdsList);
    }
}
