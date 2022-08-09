using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ImagePath { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
