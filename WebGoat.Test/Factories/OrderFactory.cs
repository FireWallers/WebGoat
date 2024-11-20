using System;
using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public static class OrderFactory
    {
        public static Order Create(int id, DateTime orderDate, string customerId)
        {
            return new Order
            {
                OrderId = id,
                OrderDate = orderDate,
                CustomerId = customerId
            };
        }
    }
}
