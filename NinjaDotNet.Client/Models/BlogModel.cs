using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDotNet.Client.Models
{
    public class BlogModel
    {
        public int BlogId { get; set; }
        [Required]
        [DisplayName("Blog Title")]
        public string Title { get; set; }
        public Guid Author { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [DisplayName("Blog Body")]
        public string BlogBody { get; set; }
        public string Synposis { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class BlogCommentModel
    {
        public int CommentId { get; set; }
        public Guid BlogId { get; set; }
        public Guid Commenter { get; set; }
        public string CommentText { get; set; }
    }
}
