using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Helpers
{
    public static class GameDurationHelper
    {
        public static string GetGameDuration(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            string secondsStr = seconds.ToString();

            if(seconds < 10) secondsStr = "0" + seconds.ToString();
            
            
            return $"{minutes} : {secondsStr}";
        }
    }
}
