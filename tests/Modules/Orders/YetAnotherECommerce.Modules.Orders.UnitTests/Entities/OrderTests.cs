using Shouldly;
using System;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Entities
{
    public class OrderTests
    {
        [Fact]
        public void AcceptOrder_WhenOrderHasStatusCreated_ThenShouldUpdateStatus()
        {
            var order = CreateOrder(OrderStatus.Created);

            order.AcceptOrder();

            order.Status.ShouldBe(OrderStatus.Accepted);
        }

        [Theory]
        [InlineData(OrderStatus.Accepted)]
        [InlineData(OrderStatus.Completed)]
        [InlineData(OrderStatus.Canceled)]
        [InlineData(OrderStatus.Rejected)]
        public void AcceptOrder_WhenOrderHasStatusDifferentThanCreated_ThenShouldThrowException(OrderStatus status)
        {
            var order = CreateOrder(status);
            var expectedException = new AcceptationNotAllowedException(order.Status);

            var exception = Assert.Throws<AcceptationNotAllowedException>(() => order.AcceptOrder());

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Theory]
        [InlineData(OrderStatus.Accepted)]
        [InlineData(OrderStatus.Created)]
        public void CancelOrder_WhenOrderHasStatusAcceptedOrCreated_ThenShouldUpdateStatus(OrderStatus status)
        {
            var order = CreateOrder(status);

            order.CancelOrder();

            order.Status.ShouldBe(OrderStatus.Canceled);
        }

        [Theory]
        [InlineData(OrderStatus.Completed)]
        [InlineData(OrderStatus.Canceled)]
        [InlineData(OrderStatus.Rejected)]
        public void CancelOrder_WhenOrderHasStatusDifferentThanAcceptedOrCreated_ThenShouldThrowException(OrderStatus status)
        {
            var order = CreateOrder(status);
            var expectedException = new CancellationNotAllowedException(order.Status);

            var exception = Assert.Throws<CancellationNotAllowedException>(() => order.CancelOrder());

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public void CompleteOrder_WhenOrderHasStatusAccepted_ThenShouldUpdateStatus()
        {
            var order = CreateOrder(OrderStatus.Accepted);

            order.CompleteOrder();

            order.Status.ShouldBe(OrderStatus.Completed);
        }

        [Theory]
        [InlineData(OrderStatus.Created)]
        [InlineData(OrderStatus.Completed)]
        [InlineData(OrderStatus.Canceled)]
        [InlineData(OrderStatus.Rejected)]
        public void CompleteOrder_WhenOrderHasStatusDifferentThanAccepted_ThenShouldThrowException(OrderStatus status)
        {
            var order = CreateOrder(status);
            var expectedException = new CompletionNotAllowedException(order.Status);

            var exception = Assert.Throws<CompletionNotAllowedException>(() => order.CompleteOrder());

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        [Fact]
        public void RejectOrder_WhenOrderHasStatusAccepted_ThenShouldUpdateStatus()
        {
            var order = CreateOrder(OrderStatus.Accepted);

            order.RejectOrder();

            order.Status.ShouldBe(OrderStatus.Rejected);
        }

        [Theory]
        [InlineData(OrderStatus.Created)]
        [InlineData(OrderStatus.Completed)]
        [InlineData(OrderStatus.Canceled)]
        [InlineData(OrderStatus.Rejected)]
        public void Reject_WhenOrderHasStatusDifferentThanAccepted_ThenShouldThrowException(OrderStatus status)
        {
            var order = CreateOrder(status);
            var expectedException = new RejectionNotAllowedException(order.Status);

            var exception = Assert.Throws<RejectionNotAllowedException>(() => order.RejectOrder());

            exception.ShouldNotBeNull();
            exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
            exception.Message.ShouldBe(expectedException.Message);
        }

        private static Order CreateOrder(OrderStatus status)
        {
            var order = Activator.CreateInstance(typeof(Order), nonPublic: true) as Order;
            var propertyInfo = typeof(Order).GetProperty(nameof(order.Status));
            propertyInfo.SetValue(order, status);

            return order;
        }
    }
}