using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.Models.ViewModels
{
    public class SinglePostViewModel
    {
        public BlogPost Post { get; set; }
        public IEnumerable<BlogPost> RelatedPosts { get; set; }
    }
}
