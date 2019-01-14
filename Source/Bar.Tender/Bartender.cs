using System;
using System.Collections.Generic;
using Bar.Inventory;
using Bar.Models;

namespace Bar.Tender {
    internal class Bartender : IBartender {
        private readonly IInventory _inventory;
        private readonly IRequestParser _requestParser;

        public Bartender(IInventory inventory, IRequestParser requestParser) {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _requestParser = requestParser ?? throw new ArgumentNullException(nameof(requestParser));
        }

        public Order ProcessRequest(string request) {
            var order = new Order() {
                Drinks = new List<Drink>()
            };

            var parsedRequest = _requestParser.Parse(request);

            if (parsedRequest == null || parsedRequest.Count == 0) { return null; }

            foreach ((var quantity, var drinkType) in parsedRequest) {
                for (var i = 0; i < quantity; i++) {
                    var drink = _inventory.GetRandomDrink(drinkType);
                    if (drink != null) {
                        order.Drinks.Add(drink);
                    }
                }
            }

            return order;
        }
    }

    public interface IBartender {
        Order ProcessRequest(string request);
    }
}
