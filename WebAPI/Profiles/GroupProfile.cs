using AutoMapper;
using WebAPI.Models;
using WebAPI.Models.Command;

namespace WebAPI.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<CreateGroupCommand, Group>();
        }
    }
}
