using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Extensions;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Modules.ContactPostModule
{
    public class ContactPostAnswerCommand : IRequest<ContactPost>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş Buraxıla Bilməz")]
        [MinLength(3, ErrorMessage ="3 simvoldan az ola bilməz")]
        public string AnswerMessage { get; set; }
        public class ContactPostAnswerCommandHandler : IRequestHandler<ContactPostAnswerCommand, ContactPost>
        {
            readonly TechShedDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IConfiguration configuration;
            public ContactPostAnswerCommandHandler(TechShedDbContext db, IActionContextAccessor ctx, IConfiguration configuration)
            {
                this.db = db;
                this.ctx = ctx;
                this.configuration = configuration;
            }

            public async Task<ContactPost> Handle(ContactPostAnswerCommand request, CancellationToken cancellationToken)
            {
               l1:
               if(!ctx.ModelIsValid())
               {
                    return new ContactPost
                    {
                        Id = request.Id,
                        AnswerMessage = request.AnswerMessage
                    };
               }

                var post = await db.ContactPosts.FirstOrDefaultAsync(cp => cp.Id == request.Id, cancellationToken);

                if (post == null)
                {
                    ctx.AddModelError("AnswerMessage", "Tapılmadı");
                    goto l1;
                }
                else if(post.AnswerDate != null)
                {
                    ctx.AddModelError("AnswerMessage", "Artıq Cavablandırılıb");
                }

                //post.AnswerById = 1;
                post.AnswerDate = DateTime.UtcNow.AddHours(4);
                post.AnswerMessage = request.AnswerMessage;

                var emailSuccess = configuration.SendMail(post.Email, "TechShed Contact Post Answer", request.AnswerMessage, cancellationToken);

                if (emailSuccess == true)
                {
                    await db.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    ctx.AddModelError("Message", "Texniki xəta baş verdi. Birazdan yenidən yoxlayın");
                }

                

                return post;
            }
        }
    }
}