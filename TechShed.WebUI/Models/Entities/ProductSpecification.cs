using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechShed.WebUI.Models.Entities
{
    public class ProductSpecification
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
        public string Value { get; set; }
    }
}
