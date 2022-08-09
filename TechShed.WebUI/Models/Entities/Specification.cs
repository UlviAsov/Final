using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class Specification : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ProductSpecification> Specifications { get; set; }
    }
}
