namespace WebAPI.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAPI.Data;
    using WebAPI.Models;
    using WebAPI.Models.Command;
    using WebAPI.Models.DTO;

    /// <summary>
    /// Defines the <see cref="PostController" />.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        /// <summary>
        /// Defines the _context.
        /// </summary>
        private readonly PigeonContext _context;

        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="PigeonContext"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public PostController(PigeonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get specified post by it's id.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{PostDto}}}"/>.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetSpecificPost([FromRoute] int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                return await Task.FromResult(Ok(_mapper.Map<PostDto>(post)));
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get all posts only made by provided user id and user's friends.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{List{PostDto}}}"/>.</returns>
        [HttpGet("{id:int}/friends")]
        public async Task<ActionResult<List<PostDto>>> GetFriendPosts([FromRoute] int id)
        {
            var FriendListMain = _context.Friends.Where(x => x.UserRequestId == id).ToList();
            var FriendListSecondary = _context.Friends.Where(x => x.UserResponderId == id).ToList();
            var FriendList = FriendListMain.Concat(FriendListSecondary).ToList();

            var FriendIds = new List<int>();
            FriendIds.Add(id);
            foreach (var user in FriendList)
            {
                if(user.UserRequestId == id)
                {
                    FriendIds.Add((int)user.UserResponderId);
                }
                if (user.UserResponderId == id)
                {
                    FriendIds.Add((int)user.UserRequestId);
                }
            }

            var posts = new List<Post>();

            foreach (var idUser in FriendIds)
            {
                var postsTemp = _context.Posts.Include(x => x.User).Where(x => x.UserId == idUser).ToList();
                foreach (var tempPost in postsTemp)
                {
                    posts.Add(tempPost);
                }
            }
            if (!posts.Any())
            {
                return NotFound("There are 0 posts made by this user's friends :(");
            }
            var FinalPosts = new List<PostDto>();
            foreach (var item in posts)
            {
                var postDto = new PostDto
                {
                    DateTime = (DateTime)item.DateTime,
                    GroupId = item.GroupId,
                    IdPost = item.Idpost,
                    MediaId = item.MediaId,
                    Text = item.Text,
                    UserFirstLastName = item.User.FirstName + " " + item.User.LastName,
                    UserId = item.UserId
                };
                FinalPosts.Add(postDto);
            }
            return Ok(FinalPosts);
        }

        /// <summary>
        /// Gets all posts from specified user.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{PostDto}}}"/>.</returns>
        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetUserAllPosts([FromRoute] int id)
        {
            var posts = _context.Posts.Include(x => x.User).Where(x => x.UserId == id);
            if (posts != null)
            {
                return await Task.FromResult(Ok(_mapper.Map<List<PostDto>>(posts)));
            }
            else
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Gets all comments from specified post id.
        /// </summary>
        [HttpGet("comment/{postID}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetPostAllComments([FromRoute] int postID)
        {
            if(_context.Posts.Find(postID) == null)
            {
                return BadRequest("Post with this id does not exist");
            }

            var comments = _context.Comments.Include(x => x.User).Where(x => x.PostId == postID);
            if (comments != null)
            {
                return await Task.FromResult(Ok(_mapper.Map<List<CommentDto>>(comments)));
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates post (groupId should be null for normal user posts | Posts made inside group should have group id)
        /// </summary>
        /// <param name="model">The model<see cref="CreatePostCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostCommand model)
        {
            if (model.UserId == null)
            {
                return BadRequest("UserId property cannot be null");
            }
            var post = _mapper.Map<Post>(model);
            post.DateTime = DateTime.Now.AddHours(2);
            if (!string.IsNullOrEmpty(model.MediaPath))
            {
                var mediaCheck = _context.Media.SingleOrDefaultAsync(p => p.MediaPath == model.MediaPath);
                if (!string.IsNullOrEmpty(mediaCheck.Result.MediaPath))
                {
                    if (_context.Media.Find(mediaCheck.Result.Idmedia) != null)
                    {
                        post.MediaId = mediaCheck.Result.Idmedia;
                    }
                    else
                    {
                        var media = new Medium
                        {
                            MediaPath = model.MediaPath
                        };
                        _context.Media.Add(media);
                        _context.SaveChanges();
                    }
                }
            }

            try
            {
                _context.Posts.Add(post);
                _context.SaveChangesAsync();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Creates comment(commentId property inside request body is used for commenting a comment(not sure if this will be implemented so
        /// it should probably be null))
        /// </summary>
        /// <param name="postID">The postID<see cref="int"/>.</param>
        /// <param name="model">The model<see cref="CreateCommentCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost("comment/{postID}")]
        public IActionResult CreateComment([FromBody] CreateCommentCommand model, [FromRoute] int postID)
        {
            if (model.UserId == null)
            {
                return BadRequest("UserId property cannot be null");
            }
            if (model.Text == null)
            {
                return BadRequest("Text property cannot be null");
            }
            if (_context.Comments.Find(model.CommentId) == null && model.CommentId != 0)
            {
                return BadRequest("Comment with this commentId property does not exist");
            }
            var comment = new Comment();


            comment = _mapper.Map<Comment>(model);
            comment.CommentId = null;
            if (model.CommentId != 0)
            {
                comment.CommentId = model.CommentId;
            }
            comment.DateTime = DateTime.Now.AddHours(2);
            comment.PostId = postID;
            if(_context.Posts.Find(postID) == null)
            {
                return BadRequest("Post with this id does not exist");
            }
            if (_context.Users.Find(model.UserId) == null)
            {
                return BadRequest("User with this id does not exist");
            }

            try
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="postID">The postID<see cref="int"/>.</param>
        /// <param name="commentID">The commentID<see cref="int"/>.</param>
        /// <param name="model">The model<see cref="UpdateCommentCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPut("comment/{postID}/{commentID}")]
        public IActionResult UpdateComment([FromBody] UpdateCommentCommand model, [FromRoute] int postID, [FromRoute] int commentID)
        {
            if (_context.Posts.Find(postID) == null)
            {
                return BadRequest("Post with this id does not exist");
            }
            if (model.Text == null)
            {
                return BadRequest("Text property cannot be null");
            }

            var comment = _context.Comments.Find(commentID);
            if(comment == null)
            {
                return BadRequest("Comment with this commentId does not exist");
            }
            try
            {
                comment.Text = model.Text;
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="postID">The postID<see cref="int"/>.</param>
        /// <param name="commentID">The commentID<see cref="int"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpDelete("comment/{postID}/{commentID}")]
        public IActionResult DeleteComment([FromRoute] int postID, [FromRoute] int commentID)
        {
            if (_context.Posts.Find(postID) == null)
            {
                return BadRequest("Post with this id does not exist");
            }

            var comment = _context.Comments.Find(commentID);
            if (comment == null)
            {
                return BadRequest("Comment with this commentId does not exist");
            }
            try
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Adds reaction to post(available reaction names:like, heart, laugh, surprised, sad, angry, smile)
        /// </summary>
        [HttpPost("reaction/{postId}/{userId}/{reactionName}")]
        public IActionResult AddPostReaction([FromRoute] int postId, int userId, string reactionName)
        {
            if(_context.Posts.Find(postId) == null)
            {
                return NotFound("Post with this postId does not exist");
            }
            if (_context.Users.Find(userId) == null)
            {
                return NotFound("User with this userId does not exist");
            }
            if (!_context.Reactions.Where(x =>x.Value == reactionName).Any())
            {
                return NotFound("Reaction with this name does not exist");
            }
            var allPostsReactions = _context.PostReactions.Where(x => x.PostId == postId).ToList();
            foreach (var item in allPostsReactions)
            {
                if(item.UserId == userId)
                {
                    return BadRequest("Reaction to this post already exists from this user");
                }
            }
            var postReaction = new PostReaction
            {
                PostId = postId,
                ReactionId = _context.Reactions.Where(x => x.Value == reactionName).First().Idreaction,
                UserId = userId
            };

            _context.PostReactions.Add(postReaction);
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Updates reaction to post(available reaction names:like, heart, laugh, surprised, sad, angry, smile)
        /// </summary>
        [HttpPut("reaction/{postId}/{userId}/{reactionName}")]
        public IActionResult UpdatePostReaction([FromRoute] int postId, int userId, string reactionName)
        {
            if (_context.Posts.Find(postId) == null)
            {
                return NotFound("Post with this postId does not exist");
            }
            if (_context.Users.Find(userId) == null)
            {
                return NotFound("User with this userId does not exist");
            }
            if (!_context.Reactions.Where(x => x.Value == reactionName).Any())
            {
                return NotFound("Reaction with this name does not exist");
            }
            var allPostsReactions = _context.PostReactions.Where(x => x.PostId == postId).ToList();
            foreach (var item in allPostsReactions)
            {
                if (item.UserId == userId)
                {
                    item.ReactionId = _context.Reactions.Where(x => x.Value == reactionName).First().Idreaction;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest("Error while trying to update post reaction");
        }

        /// <summary>
        /// Deletes user reaction to post
        /// </summary>
        [HttpDelete("reaction/{postId}/{userId}")]
        public IActionResult DeletePostReaction([FromRoute] int postId, int userId)
        {
            if (_context.Posts.Find(postId) == null)
            {
                return NotFound("Post with this postId does not exist");
            }
            if (_context.Users.Find(userId) == null)
            {
                return NotFound("User with this userId does not exist");
            }
            var allPostsReactions = _context.PostReactions.Where(x => x.PostId == postId).ToList();
            foreach (var item in allPostsReactions)
            {
                if (item.UserId == userId)
                {
                    _context.PostReactions.Remove(item);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest("Error while trying to delete post reaction");
        }

        /// <summary>
        /// Gets specific post reactions and users
        /// </summary>
        [HttpGet("reaction/{postId}")]
        public async Task<ActionResult<IEnumerable<PostReactionDto>>> GetPostReactionsAndUsers([FromRoute] int postId)
        {
            if (_context.Posts.Find(postId) == null)
            {
                return NotFound("Post with this postId does not exist");
            }
            var posts = _context.PostReactions.Include(x =>x.User).Where(x => x.PostId == postId).ToList();
            var postsFinal = new List<PostReactionDto>();
            if(posts.Count == 0)
            {
                return BadRequest("There are no reactions to this post yet");
            }
            foreach (var item in posts)
            {
                postsFinal.Add(new PostReactionDto
                {
                    FirstLastName = _context.Users.Find(item.UserId).FirstName + " " + _context.Users.Find(item.UserId).LastName,
                    IdPostReaction = item.IdpostReaction,
                    ReactionName = _context.Reactions.Find(item.ReactionId).Value
                });
            }
            return Ok(postsFinal);
        }

        /// <summary>
        /// Updates post
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="model">The model<see cref="CreatePostCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPut("update/{id:int}")]
        public IActionResult UpdatePost([FromRoute] int id, [FromBody] CreatePostCommand model)
        {
            if (model.UserId == null)
            {
                return BadRequest("UserId property cannot be null");
            }
            var post = _mapper.Map<Post>(model);
            post.Idpost = id;
            if (!string.IsNullOrEmpty(model.MediaPath))
            {
                var mediaCheck = _context.Media.SingleOrDefaultAsync(p => p.MediaPath == model.MediaPath);
                if (!string.IsNullOrEmpty(mediaCheck.Result.MediaPath))
                {
                    if (_context.Media.Find(mediaCheck.Result.Idmedia) != null)
                    {
                        post.MediaId = mediaCheck.Result.Idmedia;
                    }
                    else
                    {
                        var media = new Medium
                        {
                            MediaPath = model.MediaPath
                        };
                        _context.Media.Add(media);
                        _context.SaveChanges();
                    }
                }
            }
            post.DateTime = DateTime.Now.AddHours(2);
            try
            {
                _context.Posts.Update(post);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes post
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
