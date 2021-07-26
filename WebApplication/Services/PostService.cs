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
        Task<IList<Post>> GetGroupPost(int id);
        Task AddPost(AddPost model);
        Task AddGroupPost(AddGroupPost model);
        Task UpdatePost(int postId,AddPost model);
        Task AddComment(int postId, AddComment model);
        Task<IList<Comment>> GetCommentById(int postId);
        Task<IList<Reaction>> GetReactionById(int postId);
        Task AddReaction(AddReaction model);
        Task DeleteReaction(int postId, int userId);

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
        public async Task<IList<Post>> GetGroupPost(int id)
        {
            return await _httpService.Get<IList<Post>>($"/api/post/group/{id}");

        }
        public async Task<IList<Post>> GetFriendsUserPost(int id)
        {
            return await _httpService.Get<IList<Post>>($"/api/post/{id}/friends");

        }

        public async Task AddPost(AddPost model)
        {

            await _httpService.Post("/api/post", model);
        }
        public async Task AddGroupPost(AddGroupPost model)
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

        public async Task<IList<Reaction>> GetReactionById(int postId)
        {
            return await _httpService.Get<IList<Reaction>>($"/api/post/reaction/{postId}");
        }

        public async Task AddReaction(AddReaction model)
        {
            await _httpService.Post("/api/post/reaction", model);
        }

        public async Task DeleteReaction(int postId, int userId)
        {
            await _httpService.Delete($"/api/post/reaction/{postId}/{userId}");
        }
    }
}
