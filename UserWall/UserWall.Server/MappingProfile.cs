using AutoMapper;

using UserWall.Dto;

namespace UserWall.Server;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserPostDto, User>();

        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<PostPostDto, Post>();
    }
}
