using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class BlogCreateDTO
    {
        [Required]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Author { get; set; }
        public string ImageUrl { get; set; }
        public string Synposis { get; set; }
        public string BlogBody { get; set; }
    }

    public class BlogUpdateDTO
    {
        [Required]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Author { get; set; }
        public string ImageUrl { get; set; }
        public string Synposis { get; set; }
        public string BlogBody { get; set; }
        public IList<BlogCommentDTO> Comments { get; set; }
    }

}
