using System.Collections.Generic;

namespace Bar.Tender
{
    internal class QuantityParser : IQuantityParser
    {
        private readonly Dictionary<string, int> _numberTable = new Dictionary<string, int>()
        {
            ["zero"] = 0,
            ["nothing"] = 0,
            ["a"] = 1,
            ["another"] = 1,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
            ["ten"] = 10,
            ["eleven"] = 11,
            ["twelve"] = 12,
            ["thirteen"] = 13,
            ["fourteen"] = 14,
            ["fifteen"] = 15,
            ["sixteen"] = 16,
            ["seventeen"] = 17,
            ["eighteen"] = 18,
            ["nineteen"] = 19,
            ["twenty"] = 20,
            ["thirty"] = 30,
            ["fourty"] = 40,
            ["fifty"] = 50,
            ["sixty"] = 60,
            ["seventy"] = 70,
            ["eighty"] = 80,
            ["ninety"] = 90,
            ["hundred"] = 100,
            ["thousand"] = 1000
        };

        public List<int> Parse(string request)
        {
            var numberList = new List<int>();
            var currentlyParsingNumber = false;
            var currentIndex = -1;

            request = request.Replace("hundred and", "hundred").Replace("thousand and", "thousand").Replace('-', ' ');

            foreach (var word in request.Split(' '))
            {
                if (TryParse(word, out var number))
                {
                    if (currentlyParsingNumber)
                    {
                        numberList[currentIndex] += number;
                    }
                    else
                    {
                        numberList.Add(number);
                        currentIndex++;
                        currentlyParsingNumber = true;
                    }
                }
                else
                {
                    currentlyParsingNumber = false;
                }
            }

            return numberList;
        }

        public bool TryParse(string word, out int number) => _numberTable.TryGetValue(word, out number) || int.TryParse(word, out number);
    }

    internal interface IQuantityParser
    {
        List<int> Parse(string request);
        bool TryParse(string word, out int number);
    }
}
