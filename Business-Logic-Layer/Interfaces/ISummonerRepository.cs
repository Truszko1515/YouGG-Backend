namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerRepository
    {
        Task<Object> GetSummonerKDA(string summonerName);
    }
}
