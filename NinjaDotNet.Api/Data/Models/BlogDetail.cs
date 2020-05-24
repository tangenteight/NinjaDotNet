using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaDotNet.Api.Data.Models
{
    [Table("BlogDetail")]
    public partial class BlogDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogDetailId { get; set; }
        public string Synopsis { get; set; }
        public string BodyHtml { get; set; }
    }
}