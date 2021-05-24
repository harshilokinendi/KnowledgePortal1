using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using KnowledgePortal.Dtos;
using KnowledgePortal.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Configuration;

namespace KnowledgePortal.Api
{
    public class PostsController : ApiController
    {
        private ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
       // public IHttpActionResult IEnumerable<PostsDto> GetPosts(string searchString, int? page)
        public IHttpActionResult  GetPosts(string searchString, int? page)

        {
            var posts = (from t in _context.Posts orderby t.LastUpdated descending select t).Take(10);
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = _context.Posts.Where(s => s.TagNames.Contains(searchString));
            }

            var path = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            var userid = User.Identity.GetUserId();

            return Ok(posts.ToList().ToPagedList(page ?? 1, path).Select(Mapper.Map<Post,PostsDto>));

          //  return _context.Posts.ToList().Select(Mapper.Map<Post, PostsDto>);
        }

        // GET /api/customers/1
        public IHttpActionResult GetPosts(int id)
        {
            var post = _context.Posts.SingleOrDefault(c => c.Id == id);

            if (post == null)
                return NotFound();

            return Ok(Mapper.Map<Post, PostsDto>(post));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreatePost(PostsDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var post = Mapper.Map<PostsDto, Post>(postDto);
            _context.Posts.Add(post);
            _context.SaveChanges();

            postDto.Id = post.Id;
            return Created(new Uri(Request.RequestUri + "/" + post.Id), postDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, PostsDto postDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var postInDb = _context.Posts.SingleOrDefault(c => c.Id == id);

            if (postInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(postDto, postInDb);

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var postInDb = _context.Posts.SingleOrDefault(c => c.Id == id);

            if (postInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Posts.Remove(postInDb);
            _context.SaveChanges();
        }
    }
}