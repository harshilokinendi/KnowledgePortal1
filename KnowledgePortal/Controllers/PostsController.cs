using System;
using System.Linq;
using System.Web.Mvc;
using KnowledgePortal.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Configuration;
using System.Data.Entity;
using KnowledgePortal.ViewModel;

namespace KnowledgePortal.Controllers
{
    public class PostsController : Controller
    {

        private ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Posts/Ind/{searchString?}/{page?}")]
        public ActionResult Ind(string searchString, int? page)
        {
            var posts = (from t in _context.Posts orderby t.LastUpdated descending select t).Take(10);
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = from t in _context.Posts
                        .Where(s => s.TagNames.Contains(searchString))
                        orderby t.LastUpdated descending
                        select t;
            }

            var pagesize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);

            return View(posts.ToList().ToPagedList(page ?? 1, pagesize));
        }

        [Authorize]
        public ActionResult New()
        {
            var postmodel = new Post();
            return View("CreateForm", postmodel);
        }
        [Route("Posts/ViewPosts/{id}")]
        public ActionResult ViewPost(int id)
        {

            var post = _context.Posts.SingleOrDefault(c => c.Id == id);


            var comments = (from t in _context.Comments.Where(c => c.PostId == id) orderby t.LastUpdated descending select t).ToList();

            var viewModel = new PostCommentViewModel
            {
                posts = post,
                comments = comments
            };

            if (post == null)
                return HttpNotFound();

            Session["id"] = id;



            return View(viewModel);
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
                // post.Id = Guid.NewGuid();
                post.Userid = User.Identity.GetUserId();
                post.CreatedOn = DateTime.Now;
                post.LastUpdated = post.CreatedOn;
                _context.Posts.Add(post);
            }
            else
            {
                var postInDb = _context.Posts.Single(c => c.Id == post.Id);
                post.Userid = User.Identity.GetUserId();
                postInDb.TagNames = post.TagNames;
                postInDb.Userid = post.Userid;
                postInDb.Summary = post.Summary;
                postInDb.Description = post.Description;
                postInDb.CreatedOn = postInDb.CreatedOn;
                postInDb.LastUpdated = DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("Ind", "Posts");
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
                Userid = post.Userid,
                CreatedOn = post.CreatedOn,
                LastUpdated = DateTime.Now.Date
            };

            return View("CreateForm", viewModel);
        }
        [Authorize]
        [HttpGet]
        public ActionResult PostLiked(int id, string Userid)
        {
            bool flag = false;
            var post = _context.Posts.Find(id);
            var likedposts = (from t in _context.Likes.Where(c => c.PostId == id) select t).ToList();
            foreach (var like in likedposts)
            {
                if (like.UserId == Userid)//if a user had liked or disliked the post before(existing user)
                {
                    flag = true;
                    if (like.IsLike == true)//if user had liked the post, dislike it
                    {
                        like.IsLike = false;
                        post.like -= 1;

                    }
                    else
                    {
                        like.IsLike = true;//if he disliked it then like it
                        post.like += 1;
                    }
                }
            }
            if (flag == false)//if he is a new user to like or dislike the post
            {
                var likes = new Likes()//create a new entry in the likes table
                {
                    PostId = id,
                    UserId = Userid,
                    IsLike = true
                };
                _context.Likes.Add(likes);
                post.like += 1;
                _context.Entry(post).State = EntityState.Modified;

            }

            _context.SaveChanges();

            //return new EmptyResult();
            return RedirectToAction("ViewPost");
        }



    }
}