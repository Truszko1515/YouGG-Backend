namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerRepository
    {
        Task<object> GetSummonerKDA(string summonerName);
        Task<object> GetQueueTypes(string summonerName);
    }
}
