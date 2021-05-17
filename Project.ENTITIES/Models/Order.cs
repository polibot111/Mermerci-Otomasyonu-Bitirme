using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order:BaseEntity
    {
        public string ShippedAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public int? ShipperID { get; set; }
        public int UserCartID { get; set; }
        public string CompanyName { get; set; }
        //R.L.
        public virtual Safe Safe { get; set; }
        public virtual UserCart UserCart { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
