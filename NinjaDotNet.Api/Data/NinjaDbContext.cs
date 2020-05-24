using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NinjaDotNet.Api.Data.Models;

namespace NinjaDotNet.Api.Data
{
    public class NinjaDbContext : IdentityDbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogDetail> BlogDetails { get; set; }
        public DbSet<BlogComment> BlogComment { get; set; }

        public NinjaDbContext(DbContextOptions<NinjaDbContext> options)
            : base(options)
        {
        }
    }
}
