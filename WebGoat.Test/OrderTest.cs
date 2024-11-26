using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using WebGoatCore.Data;
using WebGoatCore.Models;

namespace WebGoat.Test
{
    [Trait("Category", "OrderTests")]
    public class OrderTest
    {
        private readonly NorthwindContext _context;
        private readonly CustomerRepository _customerRepository;
        private readonly OrderRepository _orderRepository;

        public OrderTest()
        {
            // Initialize the in-memory context and seed data
            _context = TestContext.CreateContext();
            SeedTest.SeedData(_context);
            _customerRepository = new CustomerRepository(_context); // Assuming implementation
            _orderRepository = new OrderRepository(_context, _customerRepository);
        }

        [Fact]
        public void GetOrderById_ReturnsCorrectOrder()
        {
            // Arrange
            var expectedOrderId = 1;

            // Act
            var order = _orderRepository.GetOrderById(expectedOrderId);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(expectedOrderId, order.OrderId);
            Assert.Equal("C001", order.CustomerId);
        }

        [Fact]
        public void CreateOrder_AddsOrderAndReturnsOrderId()
        {
            // Arrange
            var newOrder = new Order
            {
                OrderId = 0, // Let the repository handle ID assignment
                CustomerId = "C001",
                EmployeeId = 1,
                OrderDate = DateTime.Today,
                RequiredDate = DateTime.Today.AddDays(7),
                ShipVia = 1,
                Freight = 10.5m,
                ShipName = "Test Ship",
                ShipAddress = "123 Test Address",
                ShipCity = "Test City",
                ShipRegion = "Test Region",
                ShipPostalCode = "12345",
                ShipCountry = "Test Country",
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        ProductId = 1,
                        UnitPrice = 18.00,
                        Quantity = 2,
                        Discount = 0.0f
                    }
                }
            };

            // Act
            var newOrderId = _orderRepository.CreateOrder(newOrder); //Ignore for now Activate test when it is fixed

            // Assert
            var createdOrder = _context.Orders.Find(newOrderId);
            Assert.NotNull(createdOrder);
            Assert.Equal("C001", createdOrder.CustomerId);
            Assert.Equal(newOrderId, createdOrder.OrderId);
        }

        [Fact]
        public void CreateOrderPayment_AddsPaymentToOrder()
        {
            // Arrange
            var orderId = 1;
            var amountPaid = 50.00m;
            var creditCardNumber = "4111111111111111";
            var expirationDate = new DateTime(2025, 12, 31);
            var approvalCode = "APPROVED123";

            // Act
            _orderRepository.CreateOrderPayment(orderId, amountPaid, creditCardNumber, expirationDate, approvalCode);

            // Assert
            var payment = _context.OrderPayments.FirstOrDefault(p => p.OrderId == orderId);
            Assert.NotNull(payment);
            Assert.Equal(amountPaid, (decimal)payment.AmountPaid);
            Assert.Equal(creditCardNumber, payment.CreditCardNumber);
            Assert.Equal(approvalCode, payment.ApprovalCode);
        }

        [Fact]
        public void GetAllOrdersByCustomerId_ReturnsOrdersForCustomer()
        {
            // Arrange
            var customerId = "C001";

            // Act
            var orders = _orderRepository.GetAllOrdersByCustomerId(customerId);

            // Assert
            Assert.NotNull(orders);
            Assert.NotEmpty(orders);
            Assert.All(orders, order => Assert.Equal(customerId, order.CustomerId));
        }

        //SECURITY TEST START FROM HERE!!!

        [Fact]
        public void CreateOrder_ShouldThrowArgumentException_OnSqlInjectionAttempt()
        {
            // Arrange
            var maliciousOrder = new Order
            {
                OrderId = 0, // Let the repository handle ID assignment
                CustomerId = "C001",
                EmployeeId = 1,
                OrderDate = DateTime.Today,
                RequiredDate = DateTime.Today.AddDays(7),
                ShipVia = 1,
                Freight = 10.5m,
                ShipName = "Test Ship",
                ShipAddress = "123 Test Address",
                ShipCity = "Test City",
                ShipRegion = "Test Region",
                ShipPostalCode = "12345",
                ShipCountry = "Test Country'; DROP TABLE Orders; --", // SQL Injection attempt
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        ProductId = 1,
                        UnitPrice = 18.00,
                        Quantity = 2,
                        Discount = 0.0f
                    }
                }
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _orderRepository.CreateOrder(maliciousOrder));
            // Assert.Equal("Invalid input detected. Potential SQL injection attempt.", exception.Message);
        }

    }
}
