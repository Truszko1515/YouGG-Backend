using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Helpers
{
    public static class CapitalizeFirstLetterHelper
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string firstLetter = input.Substring(0, 1).ToUpper();
            string remainingLetters = input.Length > 1 ? input.Substring(1).ToLower() : string.Empty;

            StringBuilder stringBuilder = new StringBuilder(firstLetter);
            stringBuilder.Append(remainingLetters);

            string result = stringBuilder.ToString().Replace(" ", "");

            if (result == "Aurelionsol") result = "AurelionSol";

            return result;
        }
    }
}
