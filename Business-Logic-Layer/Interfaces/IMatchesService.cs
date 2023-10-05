namespace Business_Logic_Layer.Interfaces
{
    public interface IMatchesService
    {
        Task<IEnumerable<string>> GetMatchListByNameAsync(string summonerName);
        Task<IEnumerable<string>> GetMatchListByPUUIDAsync(string summonerPUUID);
    }
}
