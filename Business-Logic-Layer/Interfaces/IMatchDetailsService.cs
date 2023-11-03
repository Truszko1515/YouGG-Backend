namespace Business_Logic_Layer.Interfaces
{
    public interface IMatchDetailsService
    {
         Task<MatchDto> GetMatchDetailsByMatchIdAsync(string matchId);
         Task<List<MatchDto>> GetMatchDetailsListByMatchIdsAsync(IEnumerable<string> matchIdsList);
    }
}
