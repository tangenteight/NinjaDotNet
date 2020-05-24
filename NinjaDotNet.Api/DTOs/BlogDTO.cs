using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDotNet.Api.Data.Models;

namespace NinjaDotNet.Api.DTOs
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Author { get; set; }
        public string ImageUrl { get; set; }
        public BlogDetailDTO BlogDetail { get; set; }
        public virtual IList<BlogCommentDTO> Comments { get; set; }
    }
}
