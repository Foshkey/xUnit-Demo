using System;
using Microsoft.Extensions.DependencyInjection;

namespace Bar.CLI {
    internal class Program {
        internal static void Main(string[] args) {
            var startup = new Startup();
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            var customerService = provider.GetRequiredService<ICustomerService>();

            customerService.ServeCustomer();

            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
