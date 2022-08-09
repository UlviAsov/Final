using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class ProductColor : BaseEntity
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
