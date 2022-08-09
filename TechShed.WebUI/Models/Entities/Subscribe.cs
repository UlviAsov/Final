using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class Subscribe : BaseEntity
    {
        public string Email { get; set; }
        public bool EmailSended { get; set; } = false;

        public DateTime AppliedDate { get; set; }
    }
}
