using Bar.Inventory;
using Microsoft.Extensions.DependencyInjection;

namespace Bar.Tender {
    /// <summary>
    /// Extensions class for services.
    /// </summary>
    public static class ServiceExtensions {
        /// <summary>
        /// Adds the necessary services of this component to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <returns>The service collection for builder patterns.</returns>
        public static IServiceCollection AddBartenderServices(this IServiceCollection services) => services
            .AddSingleton<IBartender, Bartender>()
            .AddSingleton<IDrinkTypeParser, DrinkTypeParser>()
            .AddSingleton<IQuantityParser, QuantityParser>()
            .AddSingleton<IRequestParser, RequestParser>()
            .AddSingleton<IRequestSanitizer, RequestSanitizer>()
            .AddCurrentInventory();
    }
}
