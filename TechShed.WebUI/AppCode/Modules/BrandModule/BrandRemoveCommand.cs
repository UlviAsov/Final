using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;
using TechShed.WebUI.Models.DataContexts;

namespace TechShed.WebUI.AppCode.Modules.BrandModule
{
    public class BrandRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int Id { get; set; }
        public class BrandRemoveCommandHandler : IRequestHandler<BrandRemoveCommand, CommandJsonResponse>
        {
            readonly TechShedDbContext db;
            public BrandRemoveCommandHandler(TechShedDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(BrandRemoveCommand request, CancellationToken cancellationToken)
            {

                var entity = await db.Brands
                   .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                if (entity == null)
                {
                    return new CommandJsonResponse(true, "Qeyd movcud deil");
                }

                entity.DeletedById = 1;
                entity.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync();

                return new CommandJsonResponse(false, "Qeyd Silindi");
            }
        }
    }
}
