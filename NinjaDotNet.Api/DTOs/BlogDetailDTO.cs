using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDotNet.Api.DTOs
{
    public class BlogDetailDTO
    {
        public int BlogDetailId { get; set; }
        public Guid BlogId { get; set; }
        public string Synopsis { get; set; }
        public string BodyHtml { get; set; }
    }
}
