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
    public class BrandSingleQuery : IRequest<Brand>
    {
        public int Id { get; set; }
        public class BrandSingleQueryHandler : IRequestHandler<BrandSingleQuery, Brand>
        {
            readonly TechShedDbContext db;
            public BrandSingleQueryHandler(TechShedDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Brands
                    .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);
                return model;
            }
        }
    }
}
