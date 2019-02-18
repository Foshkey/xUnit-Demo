using System;
using System.Collections.Generic;
using Bar.Models;

namespace Bar.Tender
{
    internal class RequestParser : IRequestParser
    {
        private readonly IDrinkTypeParser _drinkTypeParser;
        private readonly IQuantityParser _quantityParser;
        private readonly IRequestSanitizer _requestSanitizer;

        public RequestParser(IDrinkTypeParser drinkTypeParser, IQuantityParser quantityParser, IRequestSanitizer requestSanitizer)
        {
            _drinkTypeParser = drinkTypeParser ?? throw new ArgumentNullException(nameof(drinkTypeParser));
            _quantityParser = quantityParser ?? throw new ArgumentNullException(nameof(quantityParser));
            _requestSanitizer = requestSanitizer ?? throw new ArgumentNullException(nameof(requestSanitizer));
        }

        public List<(int Quantity, DrinkType DrinkType)> Parse(string request)
        {
            var requestList = new List<(int Quantity, DrinkType DrinkType)>();
            var cleanRequest = _requestSanitizer.Sanitize(request);

            (int Quantity, DrinkType DrinkType) currentValue = (0, DrinkType.Unknown);

            foreach (var word in cleanRequest.Split(' '))
            {
                if (_quantityParser.TryParse(word, out var number))
                {
                    currentValue.Quantity += number;
                }
                else if (_drinkTypeParser.TryParse(word, out var drinkType))
                {
                    currentValue.DrinkType = drinkType;
                    if (currentValue.Quantity == 0) { currentValue.Quantity++; }
                    requestList.Add(currentValue);
                    currentValue = (0, DrinkType.Unknown);
                }
            }

            return requestList;
        }
    }

    internal interface IRequestParser
    {
        List<(int Quantity, DrinkType DrinkType)> Parse(string request);
    }
}
