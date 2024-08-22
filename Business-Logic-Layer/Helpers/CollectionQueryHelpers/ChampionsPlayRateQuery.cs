using Business_Logic_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Helpers.CollectionQueryHelpers
{
    public static  class ChampionsPlayRateQuery
    {
        public static Dictionary<string, (int Count, float TotalKda, int wins)> GetChampionCounts(List<MatchDto> matches, string targetPuuid)
        {
            int matchesCounter = 0;
            var championCounts = new Dictionary<string, (int Count, float TotalKda, int wins)>();

            foreach (var match in matches)
            {
                foreach (var participant in match.info.participants)
                {
                    if (participant.puuid == targetPuuid)
                    {
                        if (championCounts.ContainsKey(participant.championName))
                        {
                            championCounts[participant.championName] = (championCounts[participant.championName].Count + 1,
                                                                      championCounts[participant.championName].TotalKda + participant.challenges.kda,
                                                                      participant.win ? 
                                                                      championCounts[participant.championName].wins+1
                                                                      :
                                                                      championCounts[participant.championName].wins);
                            ++matchesCounter;
                        }
                        else
                        {
                            championCounts.Add(participant.championName, (1, participant.challenges.kda, participant.win ? 1 : 0));
                            ++matchesCounter;
                        }
                    }
                }
            }
            Console.WriteLine("Ilość przeszukanych meczy: {0}", matchesCounter);

            return championCounts;
        }

        public static List<string> GetTopChampions(Dictionary<string, (int Count, float TotalKda, int wins)> championCounts, int topCount = 3)
        {
            return championCounts
                   .Select(kv => new { Champion = kv.Key, Count = kv.Value.Count, AvgKda = kv.Value.TotalKda / kv.Value.Count })
                   .OrderByDescending(kv => kv.Count)
                   .ThenByDescending(kv => kv.AvgKda)
                   .Take(topCount)
                   .Select(kv => kv.Champion)
                   .ToList();
        }
    }
}
