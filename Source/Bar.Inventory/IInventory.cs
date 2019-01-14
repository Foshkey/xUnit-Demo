using System.Collections.Generic;
using Bar.Models;

namespace Bar.Inventory {
    public interface IInventory {
        List<Drink> GetDrinks();
    }
}
