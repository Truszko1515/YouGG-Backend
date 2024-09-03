using Business_Logic_Layer.Dtos;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Interfaces.GlobalStats;
using Data_Acces_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.InsertingGlobalStatsServices
{
    public class LaneAvgTotalCsService : ILaneAvgTotalCsService
    {

        public LaneAvgTotalCsService()
        {

        }

        public Dictionary<string, float> CalculatePositionAvgCS(List<MatchDto> matches)
        {
            var lanesWithAvgCS = new Dictionary<string, float>
            {
                {"TOP", 0},
                {"JUNGLE", 0},
                {"MIDDLE", 0},
                {"BOTTOM", 0},
                {"UTILITY", 0}
            };

            var laneCount = new Dictionary<string, int>
            {
                {"TOP", 0},
                {"JUNGLE", 0},
                {"MIDDLE", 0},
                {"BOTTOM", 0},
                {"UTILITY", 0}
            };


            foreach (var match in matches)
            {
                var participants = match.info.participants;

                foreach (var participant in participants)
                {
                    if (lanesWithAvgCS.ContainsKey(participant.teamPosition))
                    {
                        lanesWithAvgCS[participant.teamPosition] += participant.totalMinionsKilled;
                        laneCount[participant.teamPosition] += 1;

                        if (participant.teamPosition == "JUNGLE")
                        {
                            lanesWithAvgCS[participant.teamPosition] += participant.totalAllyJungleMinionsKilled + participant.totalEnemyJungleMinionsKilled;
                            laneCount[participant.teamPosition] += 1;
                        }
                    }
                }
            }

            var lanesWithAvgCSFinal = new Dictionary<string, float>();
            foreach (var lane in lanesWithAvgCS.Keys)
            {
                if (laneCount[lane] > 0)  // Aby uniknąć dzielenia przez zero
                {
                    lanesWithAvgCSFinal[lane] = lanesWithAvgCS[lane] / laneCount[lane];
                }
                else
                {
                    lanesWithAvgCSFinal[lane] = 0;  // Może być użyteczne określić że brak danych = 0 średnia
                }
            }

            return lanesWithAvgCSFinal;
        }
    }
}
