using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebGoat.Test.Factories;
using WebGoatCore.Data;
using WebGoatCore.Models;

namespace WebGoat.Test
{
    public static class SeedTest
    {
        public static void SeedData(NorthwindContext _context)
        {
            var category = CategoryFactory.Create(1, "Beverages");
            _context.Categories.Add(category);

            var customers = new List<Customer>
            {
                CustomerFactory.Create("C001", "Customer One", "Contact One"),
                CustomerFactory.Create("C002", "Customer Two", "Contact Two")
            };
            _context.Customers.AddRange(customers);

            var suppliers = new List<Supplier>
            {
                SupplierFactory.Create(1, "Supplier One", "Contact One"),
                SupplierFactory.Create(2, "Supplier Two", "Contact Two")
            };
            _context.Suppliers.AddRange(suppliers);

            var products = new List<Product>
            {
                ProductFactory.Create(1, "Chai", 1, "2", 18.00, false, 1),
                ProductFactory.Create(2, "Chang", 1, "2", 19.00, false, 1),
                ProductFactory.Create(3, "Aniseed Syrup", 1,"2", 10.00, true, 2)
            };
            _context.Products.AddRange(products);

            var orders = new List<Order>
            {
                OrderFactory.Create(1, DateTime.Today.AddDays(-5), "C001"),
                OrderFactory.Create(2, DateTime.Today.AddMonths(-2), "C002")
            };
            _context.Orders.AddRange(orders);

            var orderDetails = new List<OrderDetail>
            {
                OrderDetailFactory.Create(1, 1, 18.00, 10),
                OrderDetailFactory.Create(1, 2, 19.00, 5),
                OrderDetailFactory.Create(2, 2, 19.00, 2)
            };
            _context.OrderDetails.AddRange(orderDetails);

            _context.SaveChanges();
        }
    }
}