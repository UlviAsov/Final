using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TechShed.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string GetAppLink(this IActionContextAccessor ctx)
        {
            string scheme = ctx.ActionContext.HttpContext.Request.Scheme;
            string host = ctx.ActionContext.HttpContext.Request.Host.ToString();

            return $"{scheme}://{host}";
        }


    }
}
