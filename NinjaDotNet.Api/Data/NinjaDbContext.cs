using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NinjaDotNet.Api.Data
{
    public class NinjaDbContext : IdentityDbContext
    {
        public NinjaDbContext(DbContextOptions<NinjaDbContext> options)
            : base(options)
        {
        }
    }
}
