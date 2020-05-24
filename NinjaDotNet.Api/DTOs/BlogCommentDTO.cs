using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDotNet.Api.DTOs
{
    public class BlogCommentDTO
    {
        public int CommentId { get; set; }
        public Guid BlogId { get; set; }
        public Guid Commenter { get; set; }
        public string CommentText { get; set; }
    }
}
