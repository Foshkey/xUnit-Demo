using System.Collections.Generic;

namespace Bar.Models
{
    /// <summary>
    /// Order model.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Drinks with the order.
        /// </summary>
        public List<Drink> Drinks { get; set; }
    }
}
