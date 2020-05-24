using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace NinjaDotNet.Api.Data.Models
{
    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Author { get; set; }
        public string ImageUrl { get; set; }
        public virtual BlogDetail BlogDetail { get; set; }
        public virtual IList<BlogComment> Comments { get; set; } 
    }
}