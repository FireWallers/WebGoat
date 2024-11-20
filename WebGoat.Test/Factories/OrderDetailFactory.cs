using WebGoatCore.Models;

namespace WebGoat.Test.Factories
{
    public static class OrderDetailFactory
    {
        public static OrderDetail Create(int orderId, int productId, double unitPrice, short quantity)
        {
            return new OrderDetail
            {
                OrderId = orderId,
                ProductId = productId,
                UnitPrice = unitPrice,
                Quantity = quantity
            };
        }
    }
}
