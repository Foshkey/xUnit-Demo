using System;
using System.Collections.Generic;
using System.Linq;
using Bar.Models;

namespace Bar.CLI
{
    internal class Presenter : IPresenter
    {
        private readonly ICustomerInterface _customerInterface;

        public Presenter(ICustomerInterface customerInterface)
        {
            _customerInterface = customerInterface ?? throw new ArgumentNullException(nameof(customerInterface));
        }

        public void PresentOrder(Order order)
        {
            if (!order.IsValid())
            {
                _customerInterface.Pause();
                _customerInterface.Say("I'm sorry. We don't have anything like that.");
                return;
            }

            foreach (var drink in order.Drinks)
            {
                _customerInterface.Pause();
                _customerInterface.Say($"Here is a {drink.Name}!");
                _customerInterface.Say($"{drink.Description}");
                _customerInterface.Say($"It costs ${drink.Price:0.00}.");
            }
        }

        public void PresentTab(IEnumerable<Order> tab)
        {
            _customerInterface.Pause();
            _customerInterface.Say("Here is your tab.");
            var total = 0m;
            var index = 0;

            foreach (var order in tab)
            {
                var orderTotal = order.Drinks.Sum(drink => drink.Price);
                _customerInterface.Say($"Order {++index} total: ${orderTotal:0.00}.");
                total += orderTotal;
            }

            _customerInterface.Say($"Grand Total: ${total:0.00}.");
        }
    }

    internal interface IPresenter
    {
        void PresentOrder(Order order);
        void PresentTab(IEnumerable<Order> tab);
    }
}
