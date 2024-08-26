using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Helpers
{
    public static class SummonerMatchDetailsHelper
    {
        public static string GetTimeAgo(long gameStartTimestamp)
        {
            DateTime gameStartDateTime = DateTimeOffset.FromUnixTimeMilliseconds(gameStartTimestamp).UtcDateTime;

            // Obliczenie różnicy czasu między czasem gry a aktualnym czasem
            TimeSpan timeSinceGame = DateTime.UtcNow - gameStartDateTime;

            // Wyciągnięcie liczby dni
            int hoursAgo = (int)timeSinceGame.TotalHours;

            //var result = $"{daysAgo} dni temu.";
            string result = string.Empty;

            if(hoursAgo >= 24)
            {
                var dasyAgo = (int)timeSinceGame.TotalDays;
                result = $"{dasyAgo} dni temu";
            }
            else
            {
                 result = $"{hoursAgo} godzin temu";
            }


            return result;
        }

        public static string GetCS(int minionsKilled, int neutralMinionsKilled, int gameDuration)
        {
            // in minutes
            gameDuration = gameDuration / 60;

            int totalScore = minionsKilled + neutralMinionsKilled;
            decimal CsPerMinute = Math.Round((decimal)((decimal)totalScore / (int)gameDuration),1);

            var result = $"CS Total {totalScore} ({CsPerMinute} /min)";

            return result;
        }
    }
}
