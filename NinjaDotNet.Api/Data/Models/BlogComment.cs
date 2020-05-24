using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaDotNet.Api.Data.Models
{
    [Table("BlogComment")]
    public partial class BlogComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public Guid Commenter { get; set; }
        public string CommentText { get; set; }
    }
}