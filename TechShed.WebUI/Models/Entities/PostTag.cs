using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class PostTag : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<BlogPostTag> TagCloud { get; set; }
    }
    public class BlogPostTag
    {
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public int PostTagId { get; set; }
        public virtual PostTag PostTag { get; set; }

    }
}
