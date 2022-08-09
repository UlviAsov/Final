using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Modules.ContactPostModule
{
    public class ContactPostAllQuery : IRequest<IEnumerable<ContactPost>>
    {
        public class ContactPostAllQueryHandler : IRequestHandler<ContactPostAllQuery, IEnumerable<ContactPost>>
        {
            readonly TechShedDbContext db;
            public ContactPostAllQueryHandler(TechShedDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<ContactPost>> Handle(ContactPostAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ContactPosts.ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
