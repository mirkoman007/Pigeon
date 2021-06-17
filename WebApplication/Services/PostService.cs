using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IPostService
    {
        Task<IList<Post>> GetUserPost(int id);
        Task<IList<Post>> GetFriendsUserPost(int id);
        Task AddPost(AddPost model);

    }

    public class PostService : IPostService
    {
        private IHttpService _httpService;

        public PostService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IList<Post>> GetUserPost(int id)
        {
            return await _httpService.Get<IList<Post>>($"/api/post/user/{id}");

        }
        public async Task<IList<Post>> GetFriendsUserPost(int id)
        {
            return await _httpService.Get<IList<Post>>($"/api/post/{id}/friends");

        }

        public async Task AddPost(AddPost model)
        {
            
            await _httpService.Post("/api/post", model);
        }
    }
}
