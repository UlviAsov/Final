using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.Models.DataContexts
{
    public static class TechShedDbSeed
    {
       static internal IApplicationBuilder InitDb(this IApplicationBuilder app)
       {
            using (var scope = app.ApplicationServices.CreateScope()) {
                var db = scope.ServiceProvider.GetRequiredService<TechShedDbContext>();
                InitBrands(db);
                InitPostTags(db);
            }
            return app;
       }
      
        private static void InitPostTags(TechShedDbContext db)
        {
            if (!db.PostTags.Any())
            {



                db.PostTags.AddRange(new[]
                {
                    new PostTag{Name="sale"},
                    new PostTag{Name="tablet"},
                    new PostTag{Name="tv"},
                    new PostTag{Name="gaming pc"},
                    new PostTag{Name="laptop"}


                });
                db.SaveChanges();
            }
        }

        private static void InitBrands(TechShedDbContext db)
        {
            if(!db.Brands.Any())
            {
                db.Brands.Add(new Entities.Brand
                {
                    Name = "Apple"
                });

                db.Brands.Add(new Entities.Brand
                {
                    Name = "Samsung"
                });

                db.SaveChanges();
            }
        }
    }
}
