using Project.ENTITIES.Models;
using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.UI.Models
{
    public class PAVM
    {
        public Product product { get; set; }
        public List<Category> Categories { get; set; }
        public IPagedList<Product> PagedProducts { get; set; }
    }
}