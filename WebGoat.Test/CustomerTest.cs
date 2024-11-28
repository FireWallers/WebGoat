using System;
using System.Linq;
using WebGoat.Test.Factories;
using WebGoatCore.Data;
using WebGoatCore.Models;
using Xunit;

namespace WebGoat.Test
{
    [Trait("Category", "CustomerTests")]
    public class CustomerRepositoryTests
    {
        private readonly NorthwindContext _context;
        private readonly CustomerRepository _repository;

        public CustomerRepositoryTests()
        {
            _context = TestContext.CreateContext();
            _repository = new CustomerRepository(_context);
            SeedTest.SeedData(_context);
        }

        [Fact]
        public void GetCustomerByUsername_ShouldReturnCustomer_WhenUsernameExists()
        {
            // Act
            var result = _repository.GetCustomerByUsername("Contact One");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Contact One", result.ContactName);
        }

        [Fact]
        public void GetCustomerByCustomerId_ShouldReturnCustomer_WhenCustomerIdExists()
        {
            // Act
            var result = _repository.GetCustomerByCustomerId("C002");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("C002", result.CustomerId);
        }

        [Fact]
        public void CreateCustomer_ShouldAddCustomer_AndReturnCustomerId()
        {
            // Act
            var customerId = _repository.CreateCustomer("Test Company", "John Doe", "123 Test St", "Test City", null, "12345", "Test Country");

            // Assert
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            Assert.NotNull(customer);
            Assert.Equal("Test Company", customer.CompanyName);
            Assert.Equal("John Doe", customer.ContactName);
        }

        [Fact]
        public void CustomerIdExists_ShouldReturnTrue_WhenCustomerIdExists()
        {
            // Act
            var result = _repository.CustomerIdExists("C001");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CustomerIdExists_ShouldReturnFalse_WhenCustomerIdDoesNotExist()
        {
            // Act
            var result = _repository.CustomerIdExists("C003");

            // Assert
            Assert.False(result);
        }

        //SECURITY TEST START FROM HERE!!!
    }
}
