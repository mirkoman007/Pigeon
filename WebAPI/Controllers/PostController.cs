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
            post.DateTime = DateTime.Now;
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
            post.DateTime = DateTime.Now;
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
