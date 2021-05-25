using AutoMapper;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Models.Command;
using WebAPI.Models.DTO;

namespace WebAPI.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostCommand, Post>();
            CreateMap<Post, PostDto>();
            CreateMap<List<Post>, List<PostDto>>();
        }
    }
}
