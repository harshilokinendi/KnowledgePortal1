using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnowledgePortal.Models;

namespace KnowledgePortal.ViewModel
{
    public class PostCommentViewModel
    {
        public Post posts { get; set; }
        public IEnumerable<Comment> comments { get; set; }
    }
}