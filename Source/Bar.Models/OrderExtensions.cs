namespace Bar.Models
{
    /// <summary>
    /// Extension methods for <see cref="Order"/>.
    /// </summary>
    public static class OrderExtensions
    {
        /// <summary>
        /// Checks if the order is a valid order.
        /// </summary>
        /// <param name="order">The order to check.</param>
        /// <returns>True if the order is not null and drinks is not null or empty.</returns>
        public static bool IsValid(this Order order) => order != null && order.Drinks != null && order.Drinks.Count > 0;
    }
}
