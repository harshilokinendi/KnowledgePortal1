using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgePortal.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int like { get; set; }
    }
}