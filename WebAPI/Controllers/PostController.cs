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

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly PigeonContext _context;

        private readonly IMapper _mapper;

        public PostController(PigeonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetSpecificPost([FromRoute] int id)
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

        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetUserAllPosts([FromRoute] int id)
        {
            var posts = _context.Posts.Where(x => x.UserId == id);
            if (posts != null)
            {
                return await Task.FromResult(Ok(_mapper.Map<List<PostDto>>(posts)));
            }
            else
            {
                return NotFound();
            }
        }

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

        [HttpPut("update/{id:int}")]
        public IActionResult UpdatePost([FromRoute]int id,[FromBody] CreatePostCommand model)
        {
            if(model.UserId == null)
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
