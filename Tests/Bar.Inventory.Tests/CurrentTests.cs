using Bar.Models;
using Xunit;
using SUT = Bar.Inventory.Current;

namespace Bar.Inventory.Tests {
    public class CurrentTests {
        private SUT BuildTarget() => new SUT();

        [Fact]
        public void ShouldBeValid() {
            var drinks = BuildTarget().GetDrinks();

            Assert.NotEmpty(drinks);
            Assert.All(drinks, drink => {
                Assert.NotNull(drink.Name);
                Assert.NotNull(drink.Description);
                Assert.NotEqual(DrinkType.Unknown, drink.Type);
                Assert.True(drink.Price > 0);
            });
        }
    }
}
