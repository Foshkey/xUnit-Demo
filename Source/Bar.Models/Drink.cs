namespace Bar.Models
{
    public class Drink
    {
        /// <summary>
        /// Short description of the drink.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Name of the drink.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price for one unit of this drink.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The type of this drink.
        /// </summary>
        public DrinkType Type { get; set; }
    }
}
