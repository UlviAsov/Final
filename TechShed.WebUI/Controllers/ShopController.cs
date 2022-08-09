using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.ViewModels;

namespace TechShed.WebUI.Controllers

{
    public class ShopController : Controller
    {
        readonly TechShedDbContext db;
        public ShopController(TechShedDbContext db)
        {
            this.db = db;
        }
     
        public IActionResult Index()
        {
            var model = new ShopIndexViewModel();
            model.Brands = db.Brands.ToList();
            model.Categories = db.Categories
                .Include(c=>c.Children)
                .ToList();


            return View(model);
        }
    }
}
