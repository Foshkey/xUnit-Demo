using System;
using System.Collections.Generic;
using Bar.Models;
using Moq;
using Xunit;
using SUT = Bar.CLI.Presenter;

namespace Bar.CLI.Tests
{
    public class PresenterTests
    {
        private readonly Mock<ICustomerInterface> _customerInterfaceMock = new Mock<ICustomerInterface>();

        private SUT BuildTarget() => new SUT(_customerInterfaceMock.Object);

        [Fact]
        public void InitializeWithNullShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SUT(null));
            Assert.Contains("customerInterface", ex.Message);
        }

        [Theory]
        [InlineData("MockName", "Here's a description.", 5)]
        [InlineData("Another Drink!", "An awesome drink!", 42)]
        [InlineData("Some other drink!", "Ha!", 2)]
        public void ShouldPresentOrder(string name, string description, decimal price)
        {
            var order = new Order()
            {
                Drinks = new List<Drink>()
                {
                    new Drink() { Name = name, Description = description, Price = price }
                }
            };

            BuildTarget().PresentOrder(order);

            _customerInterfaceMock.Verify(x => x.Say($"Here is a {name}!"));
            _customerInterfaceMock.Verify(x => x.Say(description));
            _customerInterfaceMock.Verify(x => x.Say($"It costs ${price:0.00}."));
        }

        [Fact]
        public void ShouldPresentNullOrder()
        {
            BuildTarget().PresentOrder(null);
            _customerInterfaceMock.Verify(x => x.Say("I'm sorry. We don't have anything like that."));
        }


        [Fact]
        public void ShouldPresentNullDrinksOrder()
        {
            BuildTarget().PresentOrder(new Order());
            _customerInterfaceMock.Verify(x => x.Say("I'm sorry. We don't have anything like that."));
        }

        [Fact]
        public void ShouldPresentEmptyOrder()
        {
            BuildTarget().PresentOrder(new Order() { Drinks = new List<Drink>() });
            _customerInterfaceMock.Verify(x => x.Say("I'm sorry. We don't have anything like that."));
        }

        public static IEnumerable<object[]> GetPresentTabTestData()
        {
            var tab = new List<Order>()
            {
                new Order()
                {
                    Drinks = new List<Drink>()
                    {
                        new Drink() { Price = 4 },
                        new Drink() { Price = 8 }
                    }
                },
                new Order() {
                    Drinks = new List<Drink>()
                    {
                        new Drink() { Price = 5 }
                    }
                }
            };
            yield return new object[] { tab, new string[] { "Order 1 total: $12.00.", "Order 2 total: $5.00.", "Grand Total: $17.00." } };

            tab = new List<Order>()
            {
                new Order()
                {
                    Drinks = new List<Drink>()
                    {
                        new Drink() { Price = 4 }
                    }
                }
            };
            yield return new object[] { tab, new string[] { "Order 1 total: $4.00.", "Grand Total: $4.00." } };

            tab = new List<Order>();
            yield return new object[] { tab, new string[] { "Grand Total: $0.00." } };
        }

        [Theory]
        [MemberData(nameof(GetPresentTabTestData))]
        public void ShouldPresentTab(IEnumerable<Order> orders, IEnumerable<string> expectedStatements)
        {
            BuildTarget().PresentTab(orders);
            _customerInterfaceMock.Verify(x => x.Say("Here is your tab."));
            foreach (var statement in expectedStatements)
            {
                _customerInterfaceMock.Verify(x => x.Say(statement));
            }
        }
    }
}
