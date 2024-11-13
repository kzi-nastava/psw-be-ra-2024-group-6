using AutoMapper;
using BlogDomain = Explorer.Blog.Core.Domain;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain.Blogs;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogDto, BlogDomain.Blogs.Blog>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Status>(src.Status, true)))
        .ReverseMap()
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<BlogPictureDto, BlogPicture>().ReverseMap();
        CreateMap<BlogDto, BlogHomeDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Pictures.First().Url))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();


        CreateMap<RatingDto, Rating>()
            .ForMember(dest => dest.VoteType, opt => opt.MapFrom(src => Enum.Parse<VoteType>(src.VoteType, true)))
            .ReverseMap()
            .ForMember(dest => dest.VoteType, opt => opt.MapFrom(src => src.VoteType.ToString()));
    }
}