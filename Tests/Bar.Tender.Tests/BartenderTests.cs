using System;
using System.Collections.Generic;
using Bar.Inventory;
using Bar.Models;
using Moq;
using Xunit;
using SUT = Bar.Tender.Bartender;

namespace Bar.Tender.Tests
{
    public class BartenderTests
    {
        private readonly Mock<IInventory> _inventoryMock = new Mock<IInventory>();
        private readonly Mock<IRequestParser> _requestParserMock = new Mock<IRequestParser>();

        public BartenderTests()
        {
            _inventoryMock.Setup(x => x.GetDrinks()).Returns(new List<Drink>()
            {
                new Drink() { Name = "MockDrink", Type = DrinkType.Beer },
                new Drink() { Name = "MockDrink", Type = DrinkType.Cocktail },
                new Drink() { Name = "MockDrink", Type = DrinkType.Margarita },
                new Drink() { Name = "MockDrink", Type = DrinkType.Soda },
                new Drink() { Name = "MockDrink", Type = DrinkType.Whiskey },
                new Drink() { Name = "MockDrink", Type = DrinkType.Wine }
            });
        }

        private SUT BuildTarget() => new SUT(_inventoryMock.Object, _requestParserMock.Object);

        [Fact]
        public void ShouldProcessRequest()
        {
            _requestParserMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(new List<(int Quantity, DrinkType DrinkType)>() { (2, DrinkType.Beer) });

            var order = BuildTarget().ProcessRequest("MockRequest");

            Assert.Collection(order.Drinks, drink => Assert.Equal(DrinkType.Beer, drink.Type),
                                            drink => Assert.Equal(DrinkType.Beer, drink.Type));
        }

        [Fact]
        public void ShouldHandleNull()
        {
            var order = BuildTarget().ProcessRequest("MockRequest");

            Assert.Null(order);
        }

        [Fact]
        public void ShouldHandleEmpty()
        {
            _requestParserMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(new List<(int Quantity, DrinkType DrinkType)>());

            var order = BuildTarget().ProcessRequest("MockRequest");

            Assert.Null(order);
        }

        [Fact]
        public void InitializeWithNullShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new SUT(_inventoryMock.Object, null));
            Assert.Throws<ArgumentNullException>(() => new SUT(null, _requestParserMock.Object));
        }
    }
}
