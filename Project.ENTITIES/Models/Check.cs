using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Check:BaseEntity
    {
        public string CompanyName { get; set; }
        public string CheckCode { get; set; }
        public decimal Money { get; set; }
        public DateTime PayDate { get; set; }
        public string BankName { get; set; }
        public int SafeID { get; set; }
        //R.L
        public virtual Safe Safe { get; set; }
    }
}
