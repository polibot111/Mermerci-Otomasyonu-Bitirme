using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Purchase:BaseEntity
    {
        public decimal Price { get; set; }
        //R.L
        public virtual Product Product { get; set; }
    }
}
