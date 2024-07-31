namespace Business_Logic_Layer.Interfaces
{
    public interface ISummonerRepository
    {
        Task<IEnumerable<float>> GetSummonerKDA(string summonerName);
    }
}
