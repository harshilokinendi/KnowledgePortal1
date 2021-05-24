using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KnowledgePortal.Dtos
{
    public class PostsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Tags")]
        public string TagNames { get; set; }

        [Required]
        [StringLength(150)]
        public string Summary { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}