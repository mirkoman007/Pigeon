using AutoMapper;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Models.Command;
using WebAPI.Models.DTO;

namespace WebAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Gender, opt => opt.MapFrom(y => y.Gender.Name))
                .ForMember(x => x.UserType, opt => opt.MapFrom(y => y.UserType.Value))
                .ForMember(x => x.City, opt => opt.MapFrom(y => y.City.Name));

            CreateMap<User, SearchUserDto>()
                .ForMember(x => x.Gender, opt => opt.MapFrom(y => y.Gender.Name))
                .ForMember(x => x.City, opt => opt.MapFrom(y => y.City.Name))
                .ForMember(x => x.FirstLastName, opt => opt.MapFrom(y => y.FirstName + " " + y.LastName));

            CreateMap<List<User>, List<SearchUserDto>>();

            CreateMap<RegisterUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();

        }
    }
}
