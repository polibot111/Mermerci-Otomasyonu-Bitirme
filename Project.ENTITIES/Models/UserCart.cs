using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class UserCart : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string County { get; set; }
        public string District { get; set; }

        //R.L
        public virtual List<Order> Orders { get; set; }

    }
}
