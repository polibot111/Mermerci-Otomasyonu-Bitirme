using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.UI.Models
{
    public class UserCardVM
    {
        public UserCard UserCard { get; set; }
        public CompanyCard CompanyCard { get; set; }
    }
}