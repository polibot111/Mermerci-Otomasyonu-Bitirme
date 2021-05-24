﻿using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class UserCardMap:BaseMap<UserCard>
    {
        public UserCardMap()
        {
            HasOptional(x => x.CompanyCard).WithRequired(x => x.UserCard);
        }
    }
}