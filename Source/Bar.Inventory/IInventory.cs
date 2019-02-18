using System.Collections.Generic;
using Bar.Models;

namespace Bar.Inventory
{
    /// <summary>
    /// Inventory interface for getting drinks.
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Gets all of the drinks in inventory.
        /// </summary>
        List<Drink> GetDrinks();
    }
}
