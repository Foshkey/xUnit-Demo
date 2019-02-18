using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bar.Models;

namespace Bar.Tender
{
    internal class DrinkTypeParser : IDrinkTypeParser
    {

        private readonly Dictionary<string, DrinkType> _typeMapper = new Dictionary<string, DrinkType>()
        {
            ["bourbon"] = DrinkType.Whiskey,
            ["whisky"] = DrinkType.Whiskey,
            ["whiskie"] = DrinkType.Whiskey,
            ["pop"] = DrinkType.Soda
        };

        public List<DrinkType> Parse(string request)
        {
            var drinkTypes = new List<DrinkType>();

            foreach (var word in request.Split(' '))
            {
                if (TryParse(word, out var drinkType))
                {
                    drinkTypes.Add(drinkType);
                }
            }

            return drinkTypes;
        }

        public bool TryParse(string word, out DrinkType drinkType)
        {
            word = word.EndsWith("s") ? word.Substring(0, word.Length - 1) : word;
            word = Regex.Replace(word, @"[^\w]", "");
            return Enum.TryParse(word, true, out drinkType) || _typeMapper.TryGetValue(word, out drinkType);
        }
    }

    internal interface IDrinkTypeParser
    {
        List<DrinkType> Parse(string request);
        bool TryParse(string word, out DrinkType drinkType);
    }
}
