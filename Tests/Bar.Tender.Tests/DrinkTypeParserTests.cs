using Bar.Models;
using Xunit;
using SUT = Bar.Tender.DrinkTypeParser;

namespace Bar.Tender.Tests {
    public class DrinkTypeParserTests {
        private SUT BuildTarget() => new SUT();

        [Theory]
        [InlineData("beer", DrinkType.Beer)]
        [InlineData("bourbon", DrinkType.Whiskey)]
        [InlineData("cocktail", DrinkType.Cocktail)]
        [InlineData("margarita", DrinkType.Margarita)]
        [InlineData("soda", DrinkType.Soda)]
        [InlineData("wine", DrinkType.Wine)]
        [InlineData("one beer and two bourbons", DrinkType.Beer, DrinkType.Whiskey)]
        [InlineData("two beers and three glasses of wine", DrinkType.Beer, DrinkType.Wine)]
        [InlineData("a beer, a bourbon, and two glasses of wine", DrinkType.Beer, DrinkType.Whiskey, DrinkType.Wine)]
        [InlineData("a beer, another beer, and two glasses of wine", DrinkType.Beer, DrinkType.Beer, DrinkType.Wine)]
        public void ShouldParseDrinkTypes(string request, params DrinkType[] expectedDrinkTypes) {
            var drinkTypes = BuildTarget().Parse(request);

            Assert.Equal(expectedDrinkTypes.Length, drinkTypes.Count);
            for (var i = 0; i < expectedDrinkTypes.Length; i++) {
                Assert.Equal(expectedDrinkTypes[i], drinkTypes[i]);
            }
        }
    }
}
