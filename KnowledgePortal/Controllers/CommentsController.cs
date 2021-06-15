using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgePortal.Models;
using Microsoft.AspNet.Identity;

namespace KnowledgePortal.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        private ApplicationDbContext _context;

        public CommentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult New()
        {
            var commentmodel = new Comment();
            return View("CommentsForm", commentmodel);
        }
        public ActionResult SaveForm(Comment comment)
        {

            comment.PostId = (int)Session["id"];
            comment.UserId = User.Identity.GetUserId();
            comment.UserName = User.Identity.GetUserName();
            comment.LastUpdated = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("ViewPost", "Posts", new { id = comment.PostId });


        }
    }
}