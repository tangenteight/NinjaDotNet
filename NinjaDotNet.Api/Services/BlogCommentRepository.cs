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
    public class BlogCommentRepository : BaseRepo, IBlogCommentRepository
    {
        private readonly NinjaDbContext _context;
        public BlogCommentRepository(NinjaDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> Create(BlogComment entity)
        {
            var resultCount = await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BlogComment entity)
        {
            _context.BlogComment.Remove(entity);
            return await Save();
        }

        public async Task<IList<BlogComment>> FindAll()
        {
            var results = await _context.BlogComment.ToListAsync();
            return results;
        }

        public async Task<BlogComment> FindById(int id)
        {
            var result = await _context.BlogComment.Where(i => i.CommentId == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(BlogComment entity)
        {
            _context.BlogComment.Update(entity);
            return await Save();
        }
    }
}
