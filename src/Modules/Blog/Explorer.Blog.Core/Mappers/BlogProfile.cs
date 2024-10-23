using AutoMapper;
using BlogDomain = Explorer.Blog.Core.Domain;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogDto, BlogDomain.Blog>().ReverseMap();
        CreateMap<BlogPictureDto, BlogPicture>().ReverseMap();
    }
}