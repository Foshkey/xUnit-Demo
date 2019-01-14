using Bar.Inventory;
using Microsoft.Extensions.DependencyInjection;

namespace Bar.Tender {
    public static class ServiceExtensions {
        public static IServiceCollection AddBartenderServices(this IServiceCollection services) => services
            .AddSingleton<IBartender, Bartender>()
            .AddSingleton<IDrinkTypeParser, DrinkTypeParser>()
            .AddSingleton<IQuantityParser, QuantityParser>()
            .AddSingleton<IRequestParser, RequestParser>()
            .AddSingleton<IRequestSanitizer, RequestSanitizer>()
            .AddCurrentInventory();
    }
}
