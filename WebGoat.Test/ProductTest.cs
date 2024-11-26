using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebGoat.Test.Factories;
using WebGoatCore.Data;
using WebGoatCore.Models;
using Xunit;

namespace WebGoat.Test
{
    [Trait("Category", "ProductTests")]
    public class ProductRepositoryTest
    {
        private readonly NorthwindContext _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryTest()
        {
            _context = TestContext.CreateContext();
            _repository = new ProductRepository(_context);
            SeedTest.SeedData(_context);
        }

        [Fact]
        public void GetProductById_ReturnsCorrectProduct()
        {
            var product = _repository.GetProductById(1);

            Assert.NotNull(product);
            Assert.Equal("Chai", product.ProductName);
        }

        [Fact]
        public void GetTopProducts_ReturnsCorrectTopProducts()
        {
            var topProducts = _repository.GetTopProducts(2);

            Assert.Equal(2, topProducts.Count);
            Assert.Contains(topProducts, p => p.ProductName == "Chai");
            Assert.Contains(topProducts, p => p.ProductName == "Chang");
        }

        [Fact]
        public void GetAllProducts_ReturnsAllProductsOrderedByName()
        {
            var products = _repository.GetAllProducts();

            Assert.Equal(3, products.Count);
            Assert.Equal("Aniseed Syrup", products.First().ProductName);
        }

        [Fact]
        public void FindNonDiscontinuedProducts_WithFilters_ReturnsCorrectProducts()
        {
            var productsByName = _repository.FindNonDiscontinuedProducts("Chai", null);

            Assert.Single(productsByName);
            Assert.Equal("Chai", productsByName.First().ProductName);

            var productsByCategory = _repository.FindNonDiscontinuedProducts(null, 1);

            Assert.Equal(2, productsByCategory.Count);
        }

        [Fact]
        public void FindNonDiscontinuedProducts_WithoutFilters_ReturnsAllNonDiscontinuedProducts()
        {
            var products = _repository.FindNonDiscontinuedProducts(null, null);

            Assert.Equal(2, products.Count);
            Assert.DoesNotContain(products, p => p.Discontinued);
        }

        [Fact]
        public void Update_UpdatesProductSuccessfully()
        {
            var product = _repository.GetProductById(1);
            product.UnitPrice = 20.00;

            var updatedProduct = _repository.Update(product);

            Assert.Equal(20.00, updatedProduct.UnitPrice);
        }

        [Fact]
        public void Add_AddsProductSuccessfully()
        {
            Product newProduct = ProductFactory.Create(4, "New Product", 1, "2", 25.00, false, 1);

            _repository.Add(newProduct);

            var addedProduct = _repository.GetProductById(4);
            Assert.NotNull(addedProduct);
            Assert.Equal("New Product", addedProduct.ProductName);
        }

        [Fact]
        public void AddProduct_WithNegativePrice_ThrowsArgumentException()
        {
            Product invalidProduct = ProductFactory.Create(5, "Invalid Product", 1, "1", -10.00, false, 1);

            // Assuming the repository validates product data and throws ArgumentException
            var exception = Assert.Throws<ArgumentException>(() => _repository.Add(invalidProduct));

            // Assert.Equal("Product price cannot be negative.", exception.Message); //TODO Create the actualt exceptions and test for the exception is the right thrown
        }

        [Theory]
        [InlineData("Invalid*Product")]   // Invalid, contains *
        [InlineData("Invalid#Name")]      // Invalid, contains #
        [InlineData("!Invalid")]          // Invalid, starts with special character
        public void ValidateProductName_EnforcesAllowedCharacters(string productName)
        {
            Product product = ProductFactory.Create(6, productName, 1, "1", 15.00, false, 1);

            var exception = Assert.Throws<ArgumentException>(() => _repository.Add(product));
            // Assert.Equal("Product name contains invalid characters.", exception.Message); //TODO Create the actualt exceptions and test for the exception is the right thrown
        }
                
        //SECURITY TEST START FROM HERE!!!

    }
}
