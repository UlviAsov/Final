using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.Models.DataContexts;

namespace TechShed.WebUI.Controllers
{

    public class FaqsController : Controller
    {
        readonly TechShedDbContext db;
        public FaqsController(TechShedDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var faqs = db.Faqs.ToList();
            return View(faqs);
        }
    }
}
