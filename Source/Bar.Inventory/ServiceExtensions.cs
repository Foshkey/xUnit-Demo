using Microsoft.Extensions.DependencyInjection;

namespace Bar.Inventory {
    /// <summary>
    /// Extensions class for services.
    /// </summary>
    public static class ServiceExtensions {
        /// <summary>
        /// Adds the necessary services of this component to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <returns>The service collection for builder patterns.</returns>
        public static IServiceCollection AddCurrentInventory(this IServiceCollection services) => services
            .AddSingleton<IInventory, Current>();
    }
}
