using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechShed.WebUI.AppCode.Infrastructure
{
    public class BaseEntity : HistoryEntity
    {
        public int Id { get; set; }
       
    }

    public class HistoryEntity
    {
        public int? CreatedById { get; set; }
        // [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public int? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
