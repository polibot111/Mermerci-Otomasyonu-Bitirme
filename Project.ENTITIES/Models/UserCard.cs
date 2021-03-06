using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class UserCard:BaseEntity
    {

        public UserCard()
        {
            Role = UserCardRole.Member;
            ActivationCode = Guid.NewGuid();

        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string PhoneNumber { get; set; }
        public UserCardRole Role { get; set; }
        public Guid ActivationCode { get; set; }

        //Relation Properties
        public virtual CompanyCard CompanyCard { get; set; }
    }
}
