using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class Brand : BaseEntity
    {
        
        //[DataType(DataType.Text)]
        //[Display(Name="Adi")]
        [Required]
        public string Name { get; set; }
    }
}
