using Bar.Tender;
using Microsoft.Extensions.DependencyInjection;

namespace Bar.CLI
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton<ICustomerInterface, CustomerInterface>()
            .AddSingleton<ICustomerService, CustomerService>()
            .AddSingleton<IPresenter, Presenter>()
            .AddBartenderServices();
    }
}
