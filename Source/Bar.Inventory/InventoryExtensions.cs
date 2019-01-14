using System;
using System.Collections.Generic;
using System.Linq;
using Bar.Models;

namespace Bar.Inventory {
    public static class InventoryExtensions {
        public static List<Drink> GetDrinks(this IInventory inventory, DrinkType drinkType) => inventory?.GetDrinks()?.Where(drink => drink.Type == drinkType).ToList();

        public static Drink GetRandomDrink(this IInventory inventory) {
            var drinks = inventory?.GetDrinks();
            if (drinks == null || drinks.Count == 0) { return null; }

            var random = new Random();
            var index = random.Next(drinks.Count);

            return drinks[index];
        }

        public static Drink GetRandomDrink(this IInventory inventory, DrinkType drinkType) {
            var drinks = inventory?.GetDrinks(drinkType);
            if (drinks == null || drinks.Count == 0) { return null; }

            var random = new Random();
            var index = random.Next(drinks.Count);

            return drinks[index];
        }
    }
}
