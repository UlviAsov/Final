using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TechShed.WebUI.Models.DataContexts;
using TechShed.WebUI.Models.ViewModels;

namespace TechShed.WebUI.Controllers
{
    public class BlogsController : Controller
    {
        readonly TechShedDbContext db;
        public BlogsController(TechShedDbContext db)
        {
            this.db = db;
        }
      
        public IActionResult Index()
        {
            var data = db.BlogPosts
                .Where(bp => bp.DeletedById == null)
                .ToList();
            return View(data);
        }
     
        public IActionResult Details(int id)
        {
            var post = db.BlogPosts
                .Include(bp=>bp.TagCloud)
                .ThenInclude(bp=>bp.PostTag)
                .FirstOrDefault(bp => bp.DeletedById == null && bp.Id == id);


                if(post == null)
                {
                return NotFound();
                }
                var viewModel = new SinglePostViewModel();
                viewModel.Post = post;


                 var tagIdsQuery = post.TagCloud.Select(tc => tc.PostTagId);

               viewModel.RelatedPosts = db.BlogPosts
               .Include(bp=>bp.TagCloud)
               .Where(bp => bp.Id != id && bp.DeletedById == null 
               && bp.TagCloud.Any(tc => tagIdsQuery.Any(qId => qId == tc.PostTagId)))
               .OrderByDescending(bp => bp.Id)
               .Take(6)
               .ToList();



            //viewModel.RelatedPosts = (from bp in db.BlogPosts
            //                          join tc in db.BlogPostTagCloud on bp.Id equals tc.BlogPostId
            //                          where bp.Id != id  && bp.DeletedById == null && tagIdsQuery.Any(q => q == tc.PostTagId)
            //                          select bp)
            //                          .Distinct()
            //                          .ToList();



            return View(viewModel);
        }
    }
}
