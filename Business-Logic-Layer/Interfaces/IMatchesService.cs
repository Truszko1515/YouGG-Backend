namespace Business_Logic_Layer.Interfaces
{
    public interface IMatchesService
    {
        Task<IEnumerable<string>> GetMatchListByPUUIDAsync(string summonerPUUID);
    }
}
