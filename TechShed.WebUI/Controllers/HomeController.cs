using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Modules.SubscribeModule;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.Controllers
{
   
    public class HomeController : Controller
    {
        readonly TechShedDbContext db;
        readonly IMediator mediator;
        public HomeController(TechShedDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactPost model)
        {
            if(!ModelState.IsValid)
            {
                return Json(new
                {
                    error = true,
                    message= ModelState.SelectMany(ms=>ms.Value.Errors).First().ErrorMessage
                });
                
            }

            await db.ContactPosts.AddAsync(model);
            await db.SaveChangesAsync();

            return Json(new
            {
                error=false,
                message="Muracietiniz qebul olundu"
            });



        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeCreateCommand command)
        {
            var response = await mediator.Send(command);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> SubscribeConfirm(SubscribeCreateCommand command)
        {
            var response = await mediator.Send(command);
            return View(response);
        }
    }
}
