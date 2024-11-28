using System;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.DomainModels
{
    public class ShipmentDM
    {
        public int ShipmentId { get; set; }
        public int OrderId { get; set; }
        public int ShipperId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string TrackingNumber { get; set; }

        public virtual OrderDM Order { get; set; }
        public virtual ShipperDM Shipper { get; set; }
    }
}
