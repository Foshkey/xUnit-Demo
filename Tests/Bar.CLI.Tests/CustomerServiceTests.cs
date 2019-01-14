using System;
using Bar.Tender;
using Moq;
using Xunit;
using SUT = Bar.CLI.CustomerService;

namespace Bar.CLI.Tests {
    public class CustomerServiceTests {
        private readonly Mock<IBartender> _bartenderMock = new Mock<IBartender>();
        private readonly Mock<ICustomerInterface> _customerInterfaceMock = new Mock<ICustomerInterface>();
        private readonly Mock<IPresenter> _presenterMock = new Mock<IPresenter>();

        private SUT BuildTarget() => new SUT(_bartenderMock.Object, _customerInterfaceMock.Object, _presenterMock.Object);

        [Fact]
        public void InitializeWithNullShouldThrowException() {
            Assert.Throws<ArgumentNullException>(() => new SUT(null, _customerInterfaceMock.Object, _presenterMock.Object));
            Assert.Throws<ArgumentNullException>(() => new SUT(_bartenderMock.Object, null, _presenterMock.Object));
            Assert.Throws<ArgumentNullException>(() => new SUT(_bartenderMock.Object, _customerInterfaceMock.Object, null));
        }

        [Fact]
        public void ShouldTalkToCustomer() {
            _customerInterfaceMock.Setup(x => x.Listen()).Returns("exit");

            BuildTarget().ServeCustomer();

            _customerInterfaceMock.Verify(x => x.Say(It.IsAny<string>()));
        }

        [Fact]
        public void ShouldContinueConversationWithCustomer() {
            _customerInterfaceMock.SetupSequence(x => x.Listen())
                .Returns("one beer")
                .Returns("exit");

            BuildTarget().ServeCustomer();

            _customerInterfaceMock.Verify(x => x.Say(It.IsAny<string>()), Times.AtLeast(2));
        }
    }
}
