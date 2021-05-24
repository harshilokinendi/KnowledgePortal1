using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnowledgePortal.Models;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KnowledgePortal.ViewModel
{
    public class PostsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public int PostPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Posts.Count() / (double)PostPerPage));
        }
        public IEnumerable<Post> PaginatedBlogs()
        {
            int start = (CurrentPage - 1) * PostPerPage;
            return Posts.OrderBy(b => b.Id).Skip(start).Take(PostPerPage);
        }
    }
}
