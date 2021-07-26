using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.Group;

namespace WebApplication.Services
{
    public interface IGroupService
    {
        Task<IList<SearchGroup>> Search(string searchString);
        Task<Group> GetGroupById(int id);
        Task<IList<Member>> GetGroupMembers(int groupId);
        Task<IList<Member>> GetGroupAdmins(int groupId);
        Task<IList<Member>> GetGroupUsers(int groupId);
        Task AddGroupMember(AddMember model);
        Task UpdateGroupMember(AddMember model);
        Task RemoveGroupMember(int userId, int groupId);


    }
    public class GroupService: IGroupService
    {
        private IHttpService _httpService;

        public GroupService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IList<SearchGroup>> Search(string searchString)
        {
            return await _httpService.Post<IList<SearchGroup>>("/api/Group/search", new { searchString });
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _httpService.Get<Group>($"/api/Group/{id}");

        }

        public async Task<IList<Member>> GetGroupMembers(int groupId)
        {
            return await _httpService.Get<IList<Member>>($"/api/Group/{groupId}/members");
        }
        public async Task<IList<Member>> GetGroupAdmins(int groupId)
        {
            return await _httpService.Get<IList<Member>>($"/api/Group/{groupId}/admins");
        }
        public async Task<IList<Member>> GetGroupUsers(int groupId)
        {
            return await _httpService.Get<IList<Member>>($"/api/Group/{groupId}/users");
        }

        public async Task AddGroupMember(AddMember model)
        {
            await _httpService.Post("/api/Group/add", model);
        }

        public async Task UpdateGroupMember(AddMember model)
        {
            await _httpService.Put("/api/Group/update", model);
        }

        public async Task RemoveGroupMember(int userId, int groupId)
        {
            await _httpService.Delete($"/api/Group/remove/{userId}/{groupId}");
        }
    }
}
