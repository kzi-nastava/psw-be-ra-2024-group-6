using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDomain = Explorer.Blog.Core.Domain;


namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogDto, BlogDomain.Blog> ,IBlogService
    {
        public BlogService(ICrudRepository<BlogDomain.Blog> repository, IMapper mapper) : base(repository, mapper) { }

    }
}
