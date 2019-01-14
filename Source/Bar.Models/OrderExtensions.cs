namespace Bar.Models {
    /// <summary>
    /// Extension methods for <see cref="Order"/>.
    /// </summary>
    public static class OrderExtensions {
        public static bool IsValid(this Order order) => order != null && order.Drinks != null && order.Drinks.Count > 0;
    }
}
