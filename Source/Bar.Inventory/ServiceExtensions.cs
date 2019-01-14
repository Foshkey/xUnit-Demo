using Microsoft.Extensions.DependencyInjection;

namespace Bar.Inventory {
    public static class ServiceExtensions {
        public static IServiceCollection AddCurrentInventory(this IServiceCollection services) => services
            .AddSingleton<IInventory, Current>();
    }
}
