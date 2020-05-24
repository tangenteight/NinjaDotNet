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
    public class BlogDetailRepository : BaseRepo, IBlogDetailRepository
    {
        private readonly NinjaDbContext _context;
        public BlogDetailRepository(NinjaDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> Create(BlogDetail entity)
        {
            var resultCount = await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BlogDetail entity)
        {
            _context.BlogDetails.Remove(entity);
            return await Save();
        }

        public async Task<IList<BlogDetail>> FindAll()
        {
            var results = await _context.BlogDetails.ToListAsync();
            return results;
        }

        public async Task<BlogDetail> FindById(int id)
        {
            var result = await _context.BlogDetails.Where(i => i.BlogDetailId == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(BlogDetail entity)
        {
            _context.BlogDetails.Update(entity);
            return await Save();
        }
    }
}
