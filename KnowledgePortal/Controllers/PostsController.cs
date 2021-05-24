using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgePortal.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Configuration;


namespace KnowledgePortal.Controllers
{
    
    public class PostsController : Controller
    {
        // GET: Posts
        private ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [Route("Posts/{id:alpha}")]
        public ActionResult Index(string searchString, int? page)
        {
            
           var posts= (from t in _context.Posts orderby t.LastUpdated descending select t).Take(10);

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = _context.Posts.Where(s => s.TagNames.Contains(searchString));
            }

            var path=int.Parse( ConfigurationManager.AppSettings["PageSize"]);
            var userid = User.Identity.GetUserId();
            
                return View(posts.ToList().ToPagedList(page ?? 1, path));
                    
        }

        [Authorize]
        public ActionResult New()
        {
            var postmodel = new Post();
            return View("CreateForm", postmodel);
        }
        [Route("Posts/ViewPosts/{id:alpha}")]
        public ActionResult ViewPost(int id)
        {
            var post = _context.Posts.SingleOrDefault(c => c.Id == id);

            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Post post)
        {
            if (!ModelState.IsValid)
            {
                var postModel = new Post();
                return View("CreateForm", postModel);
            }
            if (post.Id == 0)
            {
                post.UserId = User.Identity.GetUserId();
                post.CreatedOn = DateTime.Now;
                post.LastUpdated = post.CreatedOn;
                _context.Posts.Add(post);
            }
            else
            {
                var postInDb = _context.Posts.Single(c => c.Id == post.Id);
                post.UserId = User.Identity.GetUserId();
                postInDb.TagNames = post.TagNames;
                postInDb.UserId= post.UserId;
                postInDb.Summary = post.Summary;
                postInDb.Description = post.Description;
                postInDb.CreatedOn = post.CreatedOn;
                postInDb.LastUpdated = post.CreatedOn;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Posts");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var post = _context.Posts.SingleOrDefault(c => c.Id == id);
           
            if (post == null)
                return HttpNotFound();

            var viewModel = new Post
            {
                Id = post.Id,
                TagNames = post.TagNames,
                Summary = post.Summary,
                Description = post.Description,
                UserId = post.UserId,
                CreatedOn = post.CreatedOn,
                LastUpdated = DateTime.Now.Date
            };

            return View("CreateForm", viewModel);
        }
    }
}