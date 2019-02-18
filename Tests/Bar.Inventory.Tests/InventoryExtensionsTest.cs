using System.Collections.Generic;
using Bar.Models;
using Xunit;

namespace Bar.Inventory.Tests
{
    public class InventoryExtensionsTest
    {

        private class MockInventory : IInventory
        {
            public List<Drink> Drinks { get; set; }
            public List<Drink> GetDrinks() => Drinks;
        }

        [Fact]
        public void ShouldGetDrinks()
        {
            var inventory = new MockInventory()
            {
                Drinks = new List<Drink>()
                {
                    new Drink() { Name = "MockDrink", Type = DrinkType.Beer },
                    new Drink() { Name = "Another", Type = DrinkType.Beer }
                }
            };

            var drinks = inventory.GetDrinks();

            Assert.Collection(drinks, drink => Assert.Equal("MockDrink", drink.Name),
                                      drink => Assert.Equal("Another", drink.Name));
        }


        [Theory]
        [InlineData(DrinkType.Beer)]
        [InlineData(DrinkType.Cocktail)]
        [InlineData(DrinkType.Margarita)]
        [InlineData(DrinkType.Soda)]
        [InlineData(DrinkType.Whiskey)]
        [InlineData(DrinkType.Wine)]
        public void ShouldGetDrinksWithType(DrinkType drinkType)
        {
            var inventory = new MockInventory()
            {
                Drinks = new List<Drink>()
                {
                    new Drink() { Name = "MockDrink", Type = drinkType },
                    new Drink() { Name = "Another", Type = drinkType },
                    new Drink() { Name = "NotThisOne", Type = DrinkType.Unknown }
                }
            };

            var drinks = inventory.GetDrinks(drinkType);

            Assert.Collection(drinks, drink => Assert.Equal("MockDrink", drink.Name),
                                      drink => Assert.Equal("Another", drink.Name));
        }

        [Fact]
        public void ShouldGetRandomDrink()
        {
            var inventory = new MockInventory()
            {
                Drinks = new List<Drink>()
            };

            for (var i = 5; i <= 10; i++)
            {
                inventory.Drinks.Add(new Drink() { Name = "MockDrink", Price = i });
            }

            for (var i = 0; i < 10000; i++)
            {
                var drink = inventory.GetRandomDrink();

                Assert.Equal("MockDrink", drink.Name);
                Assert.InRange(drink.Price, 5, 10);
            }
        }

        [Theory]
        [InlineData(DrinkType.Beer)]
        [InlineData(DrinkType.Cocktail)]
        [InlineData(DrinkType.Margarita)]
        [InlineData(DrinkType.Soda)]
        [InlineData(DrinkType.Whiskey)]
        [InlineData(DrinkType.Wine)]
        public void ShouldGetRandomDrinkWithType(DrinkType drinkType)
        {
            var inventory = new MockInventory()
            {
                Drinks = new List<Drink>()
                {
                    new Drink() { Name = "MockDrink", Type = drinkType },
                    new Drink() { Name = "NotThisDrink", Type = DrinkType.Unknown }
                }
            };

            for (var i = 0; i < 10000; i++)
            {
                var drink = inventory.GetRandomDrink(drinkType);

                Assert.Equal("MockDrink", drink.Name);
                Assert.Equal(drinkType, drink.Type);
            }
        }

        [Fact]
        public void NullInventoryShouldReturnNull()
        {
            MockInventory inventory = null;

            var drinks = inventory.GetDrinks(DrinkType.Beer);

            Assert.Null(drinks);
        }

        [Fact]
        public void EmptyInventoryShouldReturnNull()
        {
            var inventory = new MockInventory();

            var drinks = inventory.GetDrinks(DrinkType.Beer);

            Assert.Null(drinks);
        }

        [Fact]
        public void RandomDrinkWithNullInventoryShouldReturnNull()
        {
            MockInventory inventory = null;

            var drink = inventory.GetRandomDrink();

            Assert.Null(drink);
        }

        [Fact]
        public void RandomDrinkTypeWithNullInventoryShouldReturnNull()
        {
            MockInventory inventory = null;

            var drink = inventory.GetRandomDrink(DrinkType.Beer);

            Assert.Null(drink);
        }

        [Fact]
        public void NotInStockShouldReturnEmpty()
        {
            var inventory = new MockInventory()
            {
                Drinks = new List<Drink>()
                {
                    new Drink() { Name = "WineDrink", Type = DrinkType.Wine },
                    new Drink() { Name = "WhiskeyDrink", Type = DrinkType.Whiskey }
                }
            };

            var drinks = inventory.GetDrinks(DrinkType.Beer);

            Assert.Empty(drinks);
        }

        [Fact]
        public void RandomDrinkTypeNotInStockShouldReturnNull()
        {
            var inventory = new MockInventory()
            {
                Drinks = new List<Drink>()
                {
                    new Drink() { Name = "WineDrink", Type = DrinkType.Wine },
                    new Drink() { Name = "WhiskeyDrink", Type = DrinkType.Whiskey }
                }
            };

            var drink = inventory.GetRandomDrink(DrinkType.Beer);

            Assert.Null(drink);
        }
    }
}
