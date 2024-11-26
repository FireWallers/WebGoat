using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.DomainModels
{
    public class OrderDetailDM
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual OrderDM Order { get; set; }
        public virtual ProductDM Product { get; set; }

        public decimal DecimalUnitPrice => Convert.ToDecimal(this.UnitPrice);
        public decimal ExtendedPrice => DecimalUnitPrice * Convert.ToDecimal(1 - Discount) * Quantity;
    }
}
