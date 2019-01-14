using Xunit;
using SUT = Bar.Tender.RequestSanitizer;

namespace Bar.Tender.Tests {
    public class RequestSanitizerTests {
        private SUT BuildTarget() => new SUT();

        [Theory]
        [InlineData("blah", "blah")]
        [InlineData("one beer, one bourbon", "one beer one bourbon")]
        [InlineData("twenty-three", "twenty three")]
        [InlineData("23, 42", "23 42")]
        [InlineData("lots    of         spaces", "lots of spaces")]
        [InlineData("symbols@#$( @()!.in[ ]?request", "symbols in request")]
        [InlineData("ends with period.", "ends with period")]
        [InlineData("Capital Letters", "capital letters")]
        [InlineData("one beer and two glasses of wine", "one beer two glasses of wine")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ShouldSanitize(string request, string expectedResult) {
            var output = BuildTarget().Sanitize(request);
            Assert.Equal(expectedResult, output);
        }
    }
}
