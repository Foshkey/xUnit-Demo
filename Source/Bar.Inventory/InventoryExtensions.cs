using System;
using System.Collections.Generic;
using System.Linq;
using Bar.Models;

namespace Bar.Inventory
{
    /// <summary>
    /// Extensions class for <see cref="IInventory">.
    /// </summary>
    public static class InventoryExtensions
    {
        /// <summary>
        /// Gets all of the drinks of a particular drink type.
        /// </summary>
        /// <param name="inventory">The inventory of drinks.</param>
        /// <param name="drinkType">The drink type to search for.</param>
        /// <returns>Filtered list of drinks of the drink type provided.</returns>
        public static List<Drink> GetDrinks(this IInventory inventory, DrinkType drinkType) => inventory?.GetDrinks()?.Where(drink => drink.Type == drinkType).ToList();

        /// <summary>
        /// Gets a random drink in the inventory.
        /// </summary>
        /// <param name="inventory">The inventory of drinks.</param>
        /// <returns>A random drink in the inventory.</returns>
        public static Drink GetRandomDrink(this IInventory inventory)
        {
            var drinks = inventory?.GetDrinks();
            if (drinks == null || drinks.Count == 0) { return null; }

            var random = new Random();
            var index = random.Next(drinks.Count);

            return drinks[index];
        }

        /// <summary>
        /// Gets a random drink of a particular drink type in the inventory.
        /// </summary>
        /// <param name="inventory">The inventory of drinks.</param>
        /// <param name="drinkType">The drink type to filter the inventory.</param>
        /// <returns>A random drink of the drink type provided.</returns>
        public static Drink GetRandomDrink(this IInventory inventory, DrinkType drinkType)
        {
            var drinks = inventory?.GetDrinks(drinkType);
            if (drinks == null || drinks.Count == 0) { return null; }

            var random = new Random();
            var index = random.Next(drinks.Count);

            return drinks[index];
        }
    }
}
