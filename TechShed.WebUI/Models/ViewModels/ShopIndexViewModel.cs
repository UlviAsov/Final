using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.Models.ViewModels
{
    public class ShopIndexViewModel
    {
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }

    }
}
