using System;
using System.Collections.Generic;
using Bar.Models;
using Bar.Tender;

namespace Bar.CLI
{
    internal class CustomerService : ICustomerService
    {
        private readonly IBartender _bartender;
        private readonly ICustomerInterface _customerInterface;
        private readonly IPresenter _presenter;

        private readonly List<Order> _tab = new List<Order>();

        public CustomerService(IBartender bartender, ICustomerInterface customerInterface, IPresenter presenter)
        {
            _bartender = bartender ?? throw new ArgumentNullException(nameof(bartender));
            _customerInterface = customerInterface ?? throw new ArgumentNullException(nameof(customerInterface));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public void ServeCustomer()
        {
            _customerInterface.Say("Welcome to the xBar! What would you like to order?");
            GetOrder();
        }

        private void GetOrder()
        {
            // Listen to customer.
            var request = _customerInterface.Listen();

            // If the customer wishes to leave, present the tab.
            if (request == "exit" || request == "no" || request == "n")
            {
                _presenter.PresentTab(_tab);
                _customerInterface.Say("Have a nice day!");
                return;
            }

            _customerInterface.Say("Coming right up!");

            // Process request.
            var order = _bartender.ProcessRequest(request);

            // Add to tab if valid.
            if (order.IsValid())
            {
                _tab.Add(order);
            }

            // Present order.
            _presenter.PresentOrder(order);

            // Ask if there's anything else and get another order.
            _customerInterface.Pause();
            _customerInterface.Say("Would you like anything else?");
            GetOrder();
        }
    }

    internal interface ICustomerService
    {
        void ServeCustomer();
    }
}
