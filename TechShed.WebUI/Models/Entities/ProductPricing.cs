using System.ComponentModel.DataAnnotations.Schema;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class ProductPricing : HistoryEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ColorId { get; set; }
        public virtual ProductColor Color { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }

    }
}
