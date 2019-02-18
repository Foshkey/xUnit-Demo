using System.Collections.Generic;
using Xunit;

namespace Bar.Models.Tests
{
    public class OrderExtensionsTests
    {
        public static IEnumerable<object[]> ValidOrders()
        {
            yield return new object[]
            {
                new Order()
                {
                    Drinks = new List<Drink>() { new Drink() }
                }
            };
            yield return new object[]
            {
                new Order()
                {
                    Drinks = new List<Drink>()
                    {
                        new Drink(),
                        new Drink()
                    }
                }
            };
        }

        [Theory]
        [MemberData(nameof(ValidOrders))]
        public void ShouldBeValid(Order order) => Assert.True(order.IsValid());

        public static IEnumerable<object[]> InvalidOrders()
        {
            yield return new object[] { null };
            yield return new object[] { new Order() };
            yield return new object[] { new Order() { Drinks = new List<Drink>() } };
        }

        [Theory]
        [MemberData(nameof(InvalidOrders))]
        public void ShouldNotBeValid(Order order) => Assert.False(order.IsValid());
    }
}
