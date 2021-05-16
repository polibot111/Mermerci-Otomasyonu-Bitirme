using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

        public DateTime? DeleteDate { get; set; }
        public Status Status{ get; set; }

        public BaseEntity()
        {
            Status = Status.Insarted;
            CreatedTime = DateTime.Now;
        }

    }
}
