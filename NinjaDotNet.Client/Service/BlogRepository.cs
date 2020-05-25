using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using NinjaDotNet.Client.Contracts;
using NinjaDotNet.Client.Models;

namespace NinjaDotNet.Client.Service
{
    public class BlogRepository : BaseRepository<BlogModel>, IBlogRepository
    {
        public BlogRepository(IHttpClientFactory client, ILocalStorageService service) : base(client, service)
        {
        }
    }
}
