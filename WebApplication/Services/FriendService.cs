using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.Friends;

namespace WebApplication.Services
{
    public interface IFriendService
    {
        Task SendFriendRequest (int userRequestID, int userRespondID);
        Task<IList<FriendRequest>> GetFriendRequestSent(int id);
        Task<IList<FriendRequest>> GetFriendRequestReceived(int id);
        Task<IList<Friend>> GetAllFriends(int id);
        Task<String> AcceptFriendRequest(int userRequestID, int userRespondID);
        Task<String> DeclineFriendRequest(int userRequestID, int userRespondID);
        Task Unfriend(int userRequestID, int userRespondID);

    }

    public class FriendService : IFriendService
    {
        private IHttpService _httpService;

        public FriendService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task SendFriendRequest(int userRequestID, int userRespondID)
        {
            await _httpService.Post($"/api/Friends/request/send/{userRequestID}/{userRespondID}",null);
        }

        public async Task<IList<FriendRequest>> GetFriendRequestReceived(int id)
        {
            return await _httpService.Get<IList<FriendRequest>>($"/api/Friends/request/received/{id}");
        }

        public async Task<IList<FriendRequest>> GetFriendRequestSent(int id)
        {
            return await _httpService.Get<IList<FriendRequest>>($"/api/Friends/request/sent/{id}");
        }

        public async Task<IList<Friend>> GetAllFriends(int id)
        {
            return await _httpService.Get<IList<Friend>>($"/api/Friends/all/{id}");
        }

        public async Task<String> AcceptFriendRequest(int userRequestID, int userRespondID)
        {
            return await _httpService.Get<String>($"/api/Friends/accept/{userRequestID}/{userRespondID}");
        }

        public async Task<String> DeclineFriendRequest(int userRequestID, int userRespondID)
        {
            return await _httpService.Get<String>($"/api/Friends/decline/{userRequestID}/{userRespondID}");
        }
        public async Task Unfriend(int userRequestID, int userRespondID)
        {
            await _httpService.Delete($"/api/Friends/remove/{userRequestID}/{userRespondID}");
        }
    }
}
