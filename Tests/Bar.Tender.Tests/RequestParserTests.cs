using System;
using System.Collections.Generic;
using Bar.Models;
using Moq;
using Xunit;
using SUT = Bar.Tender.RequestParser;

namespace Bar.Tender.Tests {
    public class RequestParserTests {
        private readonly IDrinkTypeParser _drinkTypeParser = new DrinkTypeParser();
        private readonly IQuantityParser _quantityParser = new QuantityParser();
        private readonly Mock<IRequestSanitizer> _requestSanitizerMock = new Mock<IRequestSanitizer>();

        public RequestParserTests() {
            _requestSanitizerMock.Setup(x => x.Sanitize(It.IsAny<string>())).Returns<string>(x => x);
        }

        private SUT BuildTarget() => new SUT(_drinkTypeParser, _quantityParser, _requestSanitizerMock.Object);

        public static IEnumerable<object[]> GetParseTestData() {
            yield return new object[] { "one beer", (1, DrinkType.Beer) };
            yield return new object[] { "one beer two glasses of wine", (1, DrinkType.Beer), (2, DrinkType.Wine) };
            yield return new object[] { "beer", (1, DrinkType.Beer) };
            yield return new object[] { "twenty three beers", (23, DrinkType.Beer) };
            yield return new object[] { "twenty three beers two glasses of wine", (23, DrinkType.Beer), (2, DrinkType.Wine) };
            yield return new object[] { "a pint and a pitcher of beers", (2, DrinkType.Beer) };
            yield return new object[] { "3 beers two bourbons and wine", (3, DrinkType.Beer), (2, DrinkType.Whiskey), (1, DrinkType.Wine) };
            yield return new object[] { "2 beers two whiskies and wine", (2, DrinkType.Beer), (2, DrinkType.Whiskey), (1, DrinkType.Wine) };
            yield return new object[] { "nothing" };
        }

        [Theory]
        [MemberData(nameof(GetParseTestData))]
        public void ShouldParseRequest(string request, params (int Quantity, DrinkType DrinkType)[] expectedResults) {
            var output = BuildTarget().Parse(request);

            Assert.Equal(expectedResults.Length, output.Count);
            for (var i = 0; i < expectedResults.Length; i++) {
                Assert.Equal(expectedResults[i].Quantity, output[i].Quantity);
                Assert.Equal(expectedResults[i].DrinkType, output[i].DrinkType);
            }
        }

        [Fact]
        public void InitializeWithNullShouldThrowException() {
            Assert.Throws<ArgumentNullException>(() => new SUT(null, _quantityParser, _requestSanitizerMock.Object));
            Assert.Throws<ArgumentNullException>(() => new SUT(_drinkTypeParser, null, _requestSanitizerMock.Object));
            Assert.Throws<ArgumentNullException>(() => new SUT(_drinkTypeParser, _quantityParser, null));
        }
    }
}
