using System.Collections.Generic;
using Bar.Models;

namespace Bar.Tender.Tests
{
    internal static class OrderInitializeHelper
    {
        public static Order AddDrinks(this Order order, int quantity, DrinkType drinkType)
        {
            if (order.Drinks == null) { order.Drinks = new List<Drink>(); }

            for (var i = 0; i < quantity; i++)
            {
                order.Drinks.Add(new Drink()
                {
                    Name = "MockDrink",
                    Type = drinkType
                });
            }

            return order;
        }
    }
}
