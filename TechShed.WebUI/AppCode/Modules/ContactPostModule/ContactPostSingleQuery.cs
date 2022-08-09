using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Modules.ContactPostModule
{
    public class ContactPostSingleQuery : IRequest<ContactPost>
    {
        public int Id { get; set; }
        public class ContactPostSingleQueryHandler : IRequestHandler<ContactPostSingleQuery, ContactPost>
        {
            readonly TechShedDbContext db;
            public ContactPostSingleQueryHandler(TechShedDbContext db)
            {
                this.db = db;
            }
            public async Task<ContactPost> Handle(ContactPostSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.ContactPosts
                    .FirstOrDefaultAsync(b => b.Id == request.Id,
                    cancellationToken);
                return model;
            }
        }
    }
}

