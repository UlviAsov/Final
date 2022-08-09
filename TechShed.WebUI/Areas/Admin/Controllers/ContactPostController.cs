using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Modules.ContactPostModule;

namespace TechShed.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactPostController : Controller
    {
        readonly IMediator mediator;
        public ContactPostController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(ContactPostAllQuery query)
        {
            var data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(ContactPostSingleQuery query)
        {
            var data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Answer(ContactPostSingleQuery query)
        {
            var data = await mediator.Send(query);

            if (data.AnswerDate != null)
            {
                return RedirectToAction(nameof(Details), routeValues:new
                {
                    id = query.Id
                });
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Answer(ContactPostAnswerCommand query)
        {
            var data = await mediator.Send(query);

            if (!ModelState.IsValid)
            {
                return View(data);
            }


            return RedirectToAction(nameof(Details), routeValues: new
            {
                id = query.Id
            });
        }
    }
}
