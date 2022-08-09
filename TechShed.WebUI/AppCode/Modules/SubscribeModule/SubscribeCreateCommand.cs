using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Extensions;
using TechShed.WebUI.AppCode.Infrastructure;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Modules.SubscribeModule
{
    public class SubscribeCreateCommand : IRequest<CommandJsonResponse>
    {
        [Required(ErrorMessage = "Bos buraxila bilmez")]
        [EmailAddress(ErrorMessage = "E-poct uygun deyil")]

        public string Email { get; set; }

        public class SubscribeCreateCommandHandler : IRequestHandler<SubscribeCreateCommand, CommandJsonResponse>
        {
            readonly TechShedDbContext db;
            readonly IConfiguration configuration;
            readonly IActionContextAccessor ctx;

            public SubscribeCreateCommandHandler(TechShedDbContext db, IConfiguration configuration, 
                IActionContextAccessor ctx)
            {
                this.db = db;
                this.configuration = configuration;
                this.ctx = ctx; 
            }
            public async Task<CommandJsonResponse> Handle(SubscribeCreateCommand request, CancellationToken cancellationToken)
            {

                var subscribe = await db.Subscribes.FirstOrDefaultAsync(s=>s.Email.Equals(request.Email),cancellationToken);

                if(subscribe == null)
                {
                    subscribe = new Subscribe();
                    subscribe.Email = request.Email;
                    await db.Subscribes.AddAsync(subscribe, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                }
                else if (subscribe.EmailSended == true)
                {
                    return new CommandJsonResponse
                    {
                        Error = true,
                        Message = "Bu email istifade edilib"
                    };
                }


                string token = $"{subscribe.Id}-{subscribe.Email}".Encrypt();
                string link = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/subscribe-confirm?token={token}";

                var emailSuccess = configuration.SendMail(subscribe.Email, "TechShed Subscribe Confirmation", $"Please confirm subscribtion with <a href=\"{link}\">link</a>", cancellationToken);

                if (emailSuccess == true)
                {
                    subscribe.EmailSended = true;
                    await db.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    return new CommandJsonResponse
                    {
                        Error = true,
                        Message = "Texniki xeta bas verdi. Biraz sonra tekrar yoxlayin"
                    };
                }


                return new CommandJsonResponse
                {
                    Error = false,
                    Message = $"Aboneliyi tamamlamaq ucun \"{subscribe.Email}\"-e gonderilmis emeliyyati tamamlayin"
                };
            }


        }
    }
}
