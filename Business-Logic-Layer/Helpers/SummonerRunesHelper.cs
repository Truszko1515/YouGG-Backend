using Business_Logic_Layer.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Helpers
{
    public static class SummonerRunesHelper
    {
        public static string GetRuneIconPath(int runeId)
        {
            Type type = typeof(RuneIcons);
            var name = Enum.GetName(type, runeId);
            if (name == null) return "Nie znaleziono runy";

            var memberInfo = type.GetMember(name);
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }

            return "Description not found for given key";
        }
    }
}

