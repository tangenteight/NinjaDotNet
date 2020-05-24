using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDotNet.Api.Data;
using NinjaDotNet.Api.Data.Models;

namespace NinjaDotNet.Api.Services
{
    public abstract class BaseRepo
    {
        private readonly NinjaDbContext _context;
        protected BaseRepo(NinjaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
