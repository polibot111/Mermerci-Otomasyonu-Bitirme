using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Rebate : BaseEntity
    {
        public bool Answer { get; set; }

        //R.L
        public virtual Order Order { get; set; }
    }
}
