using Xunit;
using SUT = Bar.Tender.QuantityParser;

namespace Bar.Tender.Tests
{
    public class QuantityParserTests
    {
        private SUT BuildTarget() => new SUT();

        [Theory]
        [InlineData("six", 6)]
        [InlineData("twenty-four", 24)]
        [InlineData("thirty-two and fourty-six", 32, 46)]
        [InlineData("3 beers, 4 glasses of wine, and 2 bourbons", 3, 4, 2)]
        [InlineData("one beer and a bourbon", 1, 1)]
        [InlineData("a glass of beer and two glasses of wine", 1, 2)]
        [InlineData("a beer, another beer, and two glasses of wine", 1, 1, 2)]
        public void ShouldParseQuantity(string request, params int[] expectedResults)
        {
            var numbers = BuildTarget().Parse(request);

            Assert.Equal(expectedResults.Length, numbers.Count);

            for (var i = 0; i < expectedResults.Length; i++)
            {
                Assert.Equal(expectedResults[i], numbers[i]);
            }
        }
    }
}
