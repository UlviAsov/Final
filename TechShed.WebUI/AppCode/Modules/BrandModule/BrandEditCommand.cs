using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Modules.BrandModule
{
    public class BrandEditCommand : IRequest<Brand>
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public class BrandEditCommandHandler : IRequestHandler<BrandEditCommand, Brand>
        {
            readonly TechShedDbContext db;
            public BrandEditCommandHandler(TechShedDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandEditCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Brands
                    .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById==null, cancellationToken);

                if (entity == null)
                {
                    return null;
                }

                entity.Name = request.Name;
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
