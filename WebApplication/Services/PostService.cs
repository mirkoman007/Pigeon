using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.Post;

namespace WebApplication.Services
{
    public interface IPostService
    {
        Task<Post> GetPostById(int id);
        Task DeletePost(int id);
        Task<IList<Post>> GetUserPost(int id);
        Task<IList<Post>> GetFriendsUserPost(int id);
        Task AddPost(AddPost model);
        Task UpdatePost(int postId,AddPost model);
        Task AddComment(int postId, AddComment model);
        Task<IList<Comment>> GetCommentById(int postId);

    }

    public class PostService : IPostService
    {
        private IHttpService _httpService;

        public PostService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _httpService.Get<Post>($"/api/post/{id}");

        }
        public async Task DeletePost(int id)
        {
            await _httpService.Delete($"/api/Post/{id}");
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

        public async Task UpdatePost(int postId, AddPost model)
        {
            await _httpService.Put($"/api/post/update/{postId}", model);
        }

        public async Task AddComment(int postId, AddComment model)
        {

            await _httpService.Post($"/api/post/comment/{postId}", model);
        }
        public async Task<IList<Comment>> GetCommentById(int id)
        {
            return await _httpService.Get<IList<Comment>>($"/api/post/comment/{id}");

        }
    }
}
