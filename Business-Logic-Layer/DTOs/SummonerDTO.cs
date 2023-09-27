namespace webapi.DTOs
{
    public class SummonerDTO
    {
        //  Encrypted account ID. Max length 56 characters.
        public string accountId { get; set; }

        //  ID of the summoner icon associated with the summoner.
        public int profileIconId { get; set; }

        //  Date summoner was last modifie
        public long revisionDate { get; set; }

        //	Summoner name.
        public string name { get; set; }

        //  Encrypted summoner ID. Max length 63 characters.
        public string id { get; set; }

        //  Encrypted PUUID.Exact length of 78 characters.
        public string puuid { get; set; }

        // 	Summoner level associated with the summoner.
        public long summonerLevel { get; set; }
    }
}
