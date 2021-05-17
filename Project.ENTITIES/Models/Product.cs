using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public short UnitsInStock { get; set; }
        public int? CategoryID { get; set; }
        public Typee Typee { get; set; }

        //R.L
        public virtual List<OrderDetail> OrderDetails{ get; set; }
        public virtual Category Category { get; set; }
    }
}
