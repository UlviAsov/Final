using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TechShed.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
         async public static Task<string> SaveFile(this IWebHostEnvironment env, IFormFile file, CancellationToken cancellationToken, string prefix = null)
         {
            string fileExtension = Path.GetExtension(file.FileName);
            //if (!Directory.Exists(Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", "blog")))
            //{
            //    Directory.CreateDirectory(Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", "blog"));
            //}
            
            string name = $"{(string.IsNullOrWhiteSpace(prefix) ? "" :prefix+"")}{Guid.NewGuid()}{fileExtension}";
            string physicalPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", name);


            using (var fs = new FileStream(physicalPath, FileMode.Create, FileAccess.Write)) 
            {
                await file.CopyToAsync(fs, cancellationToken);
            }


            return name;
         }


    }
}
