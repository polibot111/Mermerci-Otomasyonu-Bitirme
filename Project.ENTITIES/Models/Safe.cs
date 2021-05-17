using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Safe:BaseEntity
    {
        public string SafeName { get; set; }
        public string SafeShortCut { get; set; }

        //R.L
        public virtual Order Order { get; set; }
        public virtual List<Check> Checks { get; set; }
    }
}
