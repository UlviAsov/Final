using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Modules.BrandModule
{
    public class BrandCreateCommand : IRequest<Brand>
    {
        public string Name { get; set; }

        public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, Brand>
        {
            readonly TechShedDbContext db;
            public BrandCreateCommandHandler(TechShedDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
            {
                var brand = new Brand();
                brand.Name = request.Name;

                await db.Brands.AddAsync(brand);

                await db.SaveChangesAsync(cancellationToken);

                return brand;
            }
        }
    }
}
