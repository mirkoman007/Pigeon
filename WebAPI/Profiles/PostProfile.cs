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
            CreateMap<CreateCommentCommand, Comment>();
            CreateMap<UpdateCommentCommand, Comment>();
            CreateMap<Post, PostDto>()
                .ForMember(x=>x.UserFirstLastName, opt => opt.MapFrom(y => y.User.FirstName + " " + y.User.LastName));
            CreateMap<Comment, CommentDto>()
                .ForMember(x => x.UserFirstLastName, opt => opt.MapFrom(y => y.User.FirstName + " " + y.User.LastName));
            CreateMap<List<Post>, List<PostDto>>();
            CreateMap<List<Comment>, List<CommentDto>>();
        }
    }
}
