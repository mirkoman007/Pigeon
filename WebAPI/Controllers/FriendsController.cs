using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly PigeonContext _context;
        private readonly IMapper _mapper;
        public FriendsController(PigeonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Main method for sending friend requests.
        /// </summary>
        /// <param name="userRequestID">User who sends the friend request</param>
        /// <param name="userRespondID">User who receives the friend request</param>
        /// <returns></returns>
        [HttpPost("request/send/{userRequestID}/{userRespondID}")]
        public async Task<IActionResult> SendFriendRequest([FromRoute] int userRequestID, [FromRoute] int userRespondID)
        {
            if (_context.Users.Find(userRequestID) == null)
            {
                return BadRequest("User for this request Id does not exist");
            };
            if (_context.Users.Find(userRespondID) == null)
            {
                return BadRequest("User for this respond Id does not exist");
            };
            if (((_context.FriendRequests.Where(x => x.UserRequestId == userRequestID).Any()) && (_context.FriendRequests.Where(x => x.UserResponderId == userRespondID).Any())) ||
                ((_context.FriendRequests.Where(x => x.UserRequestId == userRespondID).Any()) && (_context.FriendRequests.Where(x => x.UserResponderId == userRequestID).Any())))
            {
                return BadRequest("Friend request for these users already exist!");
            };

            var friendRequest = new FriendRequest
            {
                UserRequestId = userRequestID,
                UserResponderId = userRespondID,
                DateTime = DateTime.Now
            };

            var addedFriendRequest = _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Gets all friend requests for given user id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpGet("request/{id}")]
        public async Task<ActionResult<List<FriendRequestDto>>> GetAllFriendRequestForUserID([FromRoute] int id)
        {
            if (_context.Users.Find(id) == null)
            {
                return BadRequest("User with provided Id does not exist");
            }
            List<FriendRequest> friendRequests = await _context.FriendRequests.ToListAsync();
            List<FriendRequestDto> friendRequestsDto = new List<FriendRequestDto>();
            foreach (var friendRequest in friendRequests)
            {
                if (id == friendRequest.UserResponderId)
                {
                    friendRequestsDto.Add(new FriendRequestDto
                    {
                        IdfriendRequest = friendRequest.IdfriendRequest,
                        DateTime = friendRequest.DateTime,
                        UserRequestId = friendRequest.UserRequestId,
                        UserResponderId = friendRequest.UserResponderId
                    });
                }
            }
            return await Task.FromResult(Ok(friendRequestsDto));
        }
    }
}
