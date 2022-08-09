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
    public class BrandAllQuery : IRequest<IEnumerable<Brand>>
    {
        public class BrandsAllQueryHandler : IRequestHandler<BrandAllQuery, IEnumerable<Brand>>
        {
            readonly TechShedDbContext db;

            public BrandsAllQueryHandler(TechShedDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<Brand>> Handle(BrandAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Brands.Where(b => b.DeletedById == null).ToListAsync(cancellationToken);
                return model;
            }
        }
    }
}
