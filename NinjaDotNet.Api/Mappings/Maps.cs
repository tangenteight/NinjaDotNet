using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NinjaDotNet.Api.Data.Models;
using NinjaDotNet.Api.DTOs;

namespace NinjaDotNet.Api.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<Blog, BlogCreateDTO>().ReverseMap();
            CreateMap<Blog, BlogUpdateDTO>().ReverseMap();

            CreateMap<BlogComment, BlogCommentDTO>().ReverseMap();
        }
    }
}
