using System.Collections.Generic;
using Bar.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bar.Tender.Tests {
    public class IntegrationTests {

        [Fact]
        public void AllServicesShouldBeRegistered() {
            var services = new ServiceCollection().AddBartenderServices();
            var provider = services.BuildServiceProvider();
            Assert.All(services, service => provider.GetRequiredService(service.ServiceType));
        }

        public static IEnumerable<object[]> GetTestData() {
            yield return new object[] { "one beer", new Order().AddDrinks(1, DrinkType.Beer) };
            yield return new object[] { "one beer, two glasses of wine", new Order().AddDrinks(1, DrinkType.Beer).AddDrinks(2, DrinkType.Wine) };
            yield return new object[] { "beer", new Order().AddDrinks(1, DrinkType.Beer) };
            yield return new object[] { "twenty-three beers", new Order().AddDrinks(23, DrinkType.Beer) };
            yield return new object[] { "twenty-three beers and two glasses of wine", new Order().AddDrinks(23, DrinkType.Beer).AddDrinks(2, DrinkType.Wine) };
            yield return new object[] { "a pint and a pitcher of beers", new Order().AddDrinks(2, DrinkType.Beer) };
            yield return new object[] { "3 beers, two bourbons, and wine", new Order().AddDrinks(3, DrinkType.Beer).AddDrinks(2, DrinkType.Whiskey).AddDrinks(1, DrinkType.Wine) };
            yield return new object[] { "nothing", null };
            yield return new object[] { "lizard", null };
            yield return new object[] { "-1 beers", new Order().AddDrinks(1, DrinkType.Beer) };
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void ShouldBuildAndRun(string request, Order expectedOrder) {
            var services = new ServiceCollection().AddBartenderServices();
            var provider = services.BuildServiceProvider();
            var bartender = provider.GetRequiredService<IBartender>();

            var order = bartender.ProcessRequest(request);

            if (expectedOrder == null) {
                Assert.Null(order);
                return;
            }

            Assert.Equal(expectedOrder.Drinks.Count, order.Drinks.Count);
            for (var i = 0; i < expectedOrder.Drinks.Count; i++) {
                Assert.Equal(expectedOrder.Drinks[i].Type, order.Drinks[i].Type);
            }
        }
    }
}
