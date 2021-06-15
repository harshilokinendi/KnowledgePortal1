using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgePortal.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool IsLike { get; set; }
    }
}