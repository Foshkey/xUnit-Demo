using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Bar.CLI.Tests {
    public class IntegrationTests {
        [Fact]
        public void AllServicesShouldBeRegistered() {
            var startup = new Startup();
            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            Assert.All(services, service => provider.GetRequiredService(service.ServiceType));
        }

        [Theory]
        [InlineData("exit")]
        [InlineData("no")]
        [InlineData("n")]
        [InlineData("one beer", "exit")]
        [InlineData("one beer", "no")]
        [InlineData("one beer", "two beers and a glass of wine", "exit")]
        [InlineData("one beer", "lizard", "exit")]
        [InlineData("one beer", "-1 beers", "exit")]
        [InlineData("99999999999999999999 beer", "nothing", "exit")]
        [InlineData("", "exit")]
        public void CommandsShouldProcess(params string[] requests) {
            var startup = new Startup();
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var customerInterfaceMock = new Mock<ICustomerInterface>();
            services.AddSingleton(customerInterfaceMock.Object);

            var listenSequence = customerInterfaceMock.SetupSequence(x => x.Listen());
            foreach (var request in requests) {
                listenSequence = listenSequence.Returns(request);
            }

            var provider = services.BuildServiceProvider();
            var customerService = provider.GetRequiredService<ICustomerService>();

            customerService.ServeCustomer();
        }
    }
}
