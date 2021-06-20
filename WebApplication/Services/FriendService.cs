using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.Friends;

namespace WebApplication.Services
{
    public interface IFriendService
    {
        Task<IList<Friend>> GetAllFriends(int id);

    }

    public class FriendService : IFriendService
    {
        private IHttpService _httpService;

        public FriendService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IList<Friend>> GetAllFriends(int id)
        {
            return await _httpService.Get<IList<Friend>>($"/api/Friends/all/{id}");
        }
    }
}
