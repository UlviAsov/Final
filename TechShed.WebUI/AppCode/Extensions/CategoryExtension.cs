using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static HtmlString GetCategoriesRaw(this List<Category> categories)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul style=\"padding-inline-start: 0;\">");
            foreach(var category in categories.Where(c => c.ParentId == null))
            {
                AppendCategory(category, sb);
            }
            sb.Append("</ul>");

            return new HtmlString(sb.ToString());
        }
        static void AppendCategory(Category category,StringBuilder sb)
        {
            bool haschild = category.Children.Any();
            sb.Append($"<li style=\"list-style: none;\" {(haschild? "class=category" : "")}><a href=\"#\" class=\"acc\">{category.Name}</a>");

              if (haschild)
                {
                sb.Append("<div class=\"acc-body\">");
                sb.Append("<ul>");

                foreach(var item in category.Children)
                {
                    sb.Append($"<li><a href=\"#\">{item.Name}</a></li>");
                }

                sb.Append("</ul>");
                sb.Append("</div>");
            }
            sb.Append("</li>");
            sb.Append("<br>");

        }

        public  static IEnumerable<Category> GetChildren(this Category category)
        {
            if (category.ParentId != null)
            {
                yield return category;
            }
            foreach (var item in category.Children.SelectMany(c => c.GetChildren() ))
            {
                yield return item;
            }

        }
    }
}


