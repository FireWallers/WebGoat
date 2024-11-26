using System;
using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public static class ProductFactory
    {
        public static Product Create(
            int id, 
            string name, 
            int categoryId, 
            string quantityPerUnit, 
            double unitPrice, 
            bool discontinued,
            int supplierId
            )
        {
            return new Product
            {
                ProductId = id,
                ProductName = name,
                CategoryId = categoryId,
                QuantityPerUnit = quantityPerUnit,
                UnitPrice = unitPrice,
                Discontinued = discontinued,
                SupplierId = supplierId
            };
        }
    }
}
