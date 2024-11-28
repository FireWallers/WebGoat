using System.Collections.Generic;
using System.Linq;

namespace WebGoatCore.DomainModels
{
    public class CartDM
    {
        //public List<OrderDetail> OrderDetails { get; set; }
        public IDictionary<int, OrderDetailDM> OrderDetails { get; set; }

        public decimal SubTotal =>
            OrderDetails.Values.Sum(od => od.ExtendedPrice);

        public CartDM()
        {
            OrderDetails = new Dictionary<int, OrderDetailDM>();
        }
    }
}
