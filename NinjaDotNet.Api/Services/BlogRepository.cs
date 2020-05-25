using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NinjaDotNet.Api.Contracts;
using NinjaDotNet.Api.Data;
using NinjaDotNet.Api.Data.Models;

namespace NinjaDotNet.Api.Services
{
    public class BlogRepository : BaseRepo, IBlogRepository
    {
        private readonly NinjaDbContext _context;
        public BlogRepository(NinjaDbContext context)
            :base(context)
        {
            _context = context;
        }

        public async Task<bool> Create(Blog entity)
        {
            var resultCount = await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Blog entity)
        {
            _context.Blogs.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            bool exists = await _context.Blogs.AnyAsync(i => i.BlogId == id);
            return exists;
        }

        public async Task<IList<Blog>> FindAll()
        {
            var results = await _context.Blogs.ToListAsync();
            return results;
        }

        public async Task<Blog> FindById(int id)
        {
            var result = await _context.Blogs.Where(i => i.BlogId == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(Blog entity)
        {
            _context.Blogs.Update(entity);
            return await Save();
        }
    }
}
