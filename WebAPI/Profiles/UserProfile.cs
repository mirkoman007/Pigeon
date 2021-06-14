using AutoMapper;
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
            CreateMap<RegisterUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();

        }
    }
}
