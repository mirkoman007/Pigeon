using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetUserPost(int id);

    }

    public class PostService : IPostService
    {
        private IHttpService _httpService;

        public PostService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<Post>> GetUserPost(int id)
        {
            return await _httpService.Get<IEnumerable<Post>>($"/api/post/user/{id}");

        }
    }
}
