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
        public int? UserCardID { get; set; }
        public int CompanyCardID { get; set; }


        public string UserName { get; set; }
        public string Email { get; set; }

        //R.L.
        public virtual Safe Safe { get; set; }
        public virtual CompanyCard UserCard { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
