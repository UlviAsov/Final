using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Extensions;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.Entities;
using me = TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Modules.ProductModule
{
    public class ProductCreateCommand : IRequest<ProductCreateCommandResponse>
    {
 
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public int BrandId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public SpecificationKeyValue[] Specification { get; set; }
        public ProductPricing[] Pricing { get; set; }
        public ImageItem[] Images { get; set; }



        public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, ProductCreateCommandResponse>
        {
            readonly TechShedDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IWebHostEnvironment env;
            readonly IValidator<ProductCreateCommand> validator;

            public ProductCreateCommandHandler(TechShedDbContext db,
                IActionContextAccessor ctx,
                IWebHostEnvironment env,
                IValidator<ProductCreateCommand> validator)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
                this.validator = validator;
            }

            public async Task<ProductCreateCommandResponse> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
            {
                var result = validator.Validate(request);



                if (!result.IsValid)
                {
                    var response = new ProductCreateCommandResponse
                    {
                        Product = null,
                        ValidationResult = result
                    };

                    return response;
                }

                //if (!ctx.ModelIsValid())
                //{
                //    return null;
                //}

                var product = new Product();

                product.Name = request.Name;
                product.BrandId = request.BrandId;
                product.StockKeepingUnit = request.StockKeepingUnit;
                product.CategoryId = request.CategoryId;
                product.ShortDescription = request.ShortDescription;
                product.Description = request.Description;


                if (request.Specification != null && request.Specification.Length > 0)
                {
                    product.Specifications = new List<ProductSpecification>();

                    foreach (var spec in request.Specification)
                    {
                        product.Specifications.Add(new ProductSpecification
                        {
                            SpecificationId = spec.Id,
                            Value = spec.Value
                        });
                    }
                }

                if (request.Images != null && request.Images.Any(i => i.File != null))
                {
                    product.Images = new List<ProductImage>();

                    foreach (var productFile in request.Images.Where(i => i.File != null))
                    {
                        string name = await env.SaveFile(productFile.File, cancellationToken, "product");

                        product.Images.Add(new ProductImage
                        {
                            ImagePath = name,
                            IsMain = productFile.IsMain
                        });
                    }
                }
                else
                {
                    ctx.AddModelError("Images", "Sekil qeyd edilmeyib");
                    goto l1;
                }


                if (request.Pricing != null && request.Pricing.Length > 0)
                {
                    product.Pricing = new List<me.ProductPricing>();

                    foreach (var pricing in request.Pricing)
                    {
                        product.Pricing.Add(new me.ProductPricing
                        {
                            ColorId = pricing.ColorId,
                            Price = pricing.Price
                        });
                    }
                }

                await db.Products.AddAsync(product, cancellationToken);
                try
                {
                    await db.SaveChangesAsync(cancellationToken);
                    var response = new ProductCreateCommandResponse
                    {
                        Product = product,
                        ValidationResult = result
                    };
                    return response;
                }
                catch (Exception ex)
                {
                    var response = new ProductCreateCommandResponse
                    {
                        Product = product,
                        ValidationResult = result
                    };

                    response.ValidationResult.Errors.Add(new ValidationFailure("Name", "Xeta bash verdi,Biraz sonra yeniden yoxlayin"));

                    return response;
                }

            l1:
                return null;
            }
        }
    }

    public class SpecificationKeyValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ProductPricing
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public double Price { get; set; }
    }

    public class ImageItem
    {
        public int? Id { get; set; }
        public bool IsMain { get; set; }
        public string TempPath { get; set; }
        public IFormFile File { get; set; }
    }

    public class ProductCreateCommandResponse
    {
        public Product Product { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
