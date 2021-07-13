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
    using WebAPI.Models.DTO;

    /// <summary>
    /// Defines the <see cref="FriendsController" />.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FriendsController : ControllerBase
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
        /// Initializes a new instance of the <see cref="FriendsController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="PigeonContext"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public FriendsController(PigeonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Main method for sending friend requests.
        /// </summary>
        /// <param name="userRequestID">User who sends the friend request.</param>
        /// <param name="userRespondID">User who receives the friend request.</param>
        /// <returns>.</returns>
        [HttpPost("request/send/{userRequestID}/{userRespondID}")]
        public async Task<IActionResult> SendFriendRequest([FromRoute] int userRequestID, [FromRoute] int userRespondID)
        {
            if ((_context.Friends.Where(x => x.UserRequestId == userRequestID).Where(x => x.UserResponderId == userRespondID).Any())
                || (_context.Friends.Where(x => x.UserRequestId == userRespondID).Where(x => x.UserResponderId == userRequestID).Any()))
            {
                return BadRequest("These users are already friends!");
            }

            if (_context.Users.Find(userRequestID) == null)
            {
                return BadRequest("User for this request Id does not exist");
            };
            if (_context.Users.Find(userRespondID) == null)
            {
                return BadRequest("User for this respond Id does not exist");
            };
            if (((_context.FriendRequests.Where(x => x.UserRequestId == userRequestID).Where(x => x.UserResponderId == userRespondID).Any())) ||
                ((_context.FriendRequests.Where(x => x.UserRequestId == userRespondID).Where(x => x.UserResponderId == userRequestID).Any())))
            {
                return BadRequest("Friend request for these users already exist!");
            };

            var friendRequest = new FriendRequest
            {
                UserRequestId = userRequestID,
                UserResponderId = userRespondID,
                DateTime = DateTime.Now.AddHours(2)
            };

            var addedFriendRequest = _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();
            return Ok();
        }


        /// <summary>
        /// Gets all friend requests for given user id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>.</returns>
        [HttpGet("request/all/{id}")]
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
                if (id == friendRequest.UserResponderId || id == friendRequest.UserRequestId)
                {
                    friendRequestsDto.Add(new FriendRequestDto
                    {
                        IdfriendRequest = friendRequest.IdfriendRequest,
                        DateTime = friendRequest.DateTime,
                        UserRequestId = friendRequest.UserRequestId,
                        UserRequestName = _context.Users.Find(friendRequest.UserRequestId).FirstName + ' ' + _context.Users.Find(friendRequest.UserRequestId).LastName,
                        UserResponderId = friendRequest.UserResponderId,
                        UserResponderName = _context.Users.Find(friendRequest.UserResponderId).FirstName + ' ' + _context.Users.Find(friendRequest.UserResponderId).LastName,
                    });
                }
            }
            return await Task.FromResult(Ok(friendRequestsDto));
        }


        /// <summary>
        /// Gets only receiving friend requests for provided user id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>.</returns>
        [HttpGet("request/received/{id}")]
        public async Task<ActionResult<List<FriendRequestDto>>> GetReceiveFriendRequestForUserID([FromRoute] int id)
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
                        UserRequestName = _context.Users.Find(friendRequest.UserRequestId).FirstName + ' ' + _context.Users.Find(friendRequest.UserRequestId).LastName,
                        UserResponderId = friendRequest.UserResponderId,
                        UserResponderName = _context.Users.Find(friendRequest.UserResponderId).FirstName + ' ' + _context.Users.Find(friendRequest.UserResponderId).LastName,
                    });
                }
            }
            return await Task.FromResult(Ok(friendRequestsDto));
        }

        /// <summary>
        /// Gets only friend requests that were sent by provided user id towards other users.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>.</returns>
        [HttpGet("request/sent/{id}")]
        public async Task<ActionResult<List<FriendRequestDto>>> GetSentFriendRequestForUserID([FromRoute] int id)
        {
            if (_context.Users.Find(id) == null)
            {
                return BadRequest("User with provided Id does not exist");
            }
            List<FriendRequest> friendRequests = await _context.FriendRequests.ToListAsync();
            List<FriendRequestDto> friendRequestsDto = new List<FriendRequestDto>();
            foreach (var friendRequest in friendRequests)
            {
                if (id == friendRequest.UserRequestId)
                {
                    friendRequestsDto.Add(new FriendRequestDto
                    {
                        IdfriendRequest = friendRequest.IdfriendRequest,
                        DateTime = friendRequest.DateTime,
                        UserRequestId = friendRequest.UserRequestId,
                        UserRequestName = _context.Users.Find(friendRequest.UserRequestId).FirstName + ' ' + _context.Users.Find(friendRequest.UserRequestId).LastName,
                        UserResponderId = friendRequest.UserResponderId,
                        UserResponderName = _context.Users.Find(friendRequest.UserResponderId).FirstName + ' ' + _context.Users.Find(friendRequest.UserResponderId).LastName,
                    });
                }
            }
            return await Task.FromResult(Ok(friendRequestsDto));
        }


        /// <summary>
        /// Gets list of all friends for given user id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>.</returns>
        [HttpGet("all/{id}")]
        public async Task<ActionResult<List<FriendDto>>> GetAllFriendsForUserID([FromRoute] int id)
        {
            var FriendListMain = _context.Friends.Include(x => x.UserResponder).Include(x => x.UserRequest).Where(x => x.UserRequestId == id).ToList();
            var FriendListSecondary = _context.Friends.Include(x => x.UserResponder).Include(x => x.UserRequest).Where(x => x.UserResponderId == id).ToList();
            var FriendList = FriendListMain.Union(FriendListSecondary).ToList();

            var Friends = new List<FriendDto>();
            foreach (var user in FriendList)
            {
                if (user.UserRequestId == id)
                {
                    var friendTemp = new FriendDto
                    {
                        FirstLastName = user.UserResponder.FirstName + " " + user.UserResponder.LastName,
                        UserId = (int)user.UserResponderId
                    };
                    Friends.Add(friendTemp);
                }
                if (user.UserResponderId == id)
                {
                    var friendTemp = new FriendDto
                    {
                        FirstLastName = user.UserRequest.FirstName + " " + user.UserRequest.LastName,
                        UserId = (int)user.UserRequestId
                    };
                    Friends.Add(friendTemp);
                }
            }
            return await Task.FromResult(Ok(Friends.OrderBy(x => x.FirstLastName)));
        }

        /// <summary>
        /// Gets list of all non-friends for given user id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>.</returns>
        [HttpGet("non/{id}")]
        public async Task<ActionResult<List<FriendDto>>> GetAllNonFriendsForUserID([FromRoute] int id)
        {
            var FriendListMain = _context.Friends.Include(x => x.UserResponder).Include(x => x.UserRequest).Where(x => x.UserRequestId == id).ToList();
            var FriendListSecondary = _context.Friends.Include(x => x.UserResponder).Include(x => x.UserRequest).Where(x => x.UserResponderId == id).ToList();
            var FriendList = FriendListMain.Union(FriendListSecondary).ToList();
            var allUsers = _context.Users.ToList();

            var Friends = new List<FriendDto>();
            foreach (var user in FriendList)
            {
                if (user.UserRequestId == id)
                {
                    var friendTemp = new FriendDto
                    {
                        FirstLastName = user.UserResponder.FirstName + " " + user.UserResponder.LastName,
                        UserId = (int)user.UserResponderId
                    };
                    Friends.Add(friendTemp);
                }
                if (user.UserResponderId == id)
                {
                    var friendTemp = new FriendDto
                    {
                        FirstLastName = user.UserRequest.FirstName + " " + user.UserRequest.LastName,
                        UserId = (int)user.UserRequestId
                    };
                    Friends.Add(friendTemp);
                }
            }

            var NonFriends = new List<FriendDto>();
            foreach (var user in allUsers)
            {
                var friendTemp = new FriendDto
                {
                    FirstLastName = user.FirstName + ' ' + user.LastName,
                    UserId = user.Iduser
                };
                NonFriends.Add(friendTemp);
            }

            foreach (var item in Friends)
            {
                NonFriends.RemoveAll((x) => x.UserId == item.UserId);
            }
            NonFriends.RemoveAll((x) => x.UserId == id);
            var result = NonFriends.OrderBy(x => x.FirstLastName);
            return await Task.FromResult(Ok(result));
        }

        /// <summary>
        /// Accept friend request based on friend request id.
        /// </summary>
        /// <param name="friendRequestId">User id.</param>
        /// <returns>.</returns>
        [HttpGet("accept/{friendRequestId}")]
        public async Task<ActionResult<List<FriendRequestDto>>> AcceptFriendRequest([FromRoute] int friendRequestId)
        {
            var friendRequest = _context.FriendRequests.Find(friendRequestId);
            if (friendRequest == null)
            {
                return BadRequest("Friend request with provided Id does not exist");
            }
            var friend = new Friend
            {
                DateTime = DateTime.Now.AddHours(2),
                UserRequestId = friendRequest.UserRequestId,
                UserResponderId = friendRequest.UserResponderId
            };
            try
            {
                _context.FriendRequests.Remove(friendRequest);
                await _context.Friends.AddAsync(friend);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Error while trying to add friends");
            }

            return await Task.FromResult(Ok("Friends added successfully!"));
        }

        /// <summary>
        /// Accept friend request based on user request id and user respond id.
        /// </summary>
        /// <param name="userOneId">The userOneId<see cref="int"/>.</param>
        /// <param name="userTwoId">The userTwoId<see cref="int"/>.</param>
        /// <returns>.</returns>
        [HttpGet("accept/{userOneId}/{userTwoId}")]
        public async Task<ActionResult<List<FriendRequestDto>>> AcceptFriendRequestBasedOnIds([FromRoute] int userOneId, [FromRoute] int userTwoId)
        {
            var friendRequestOne = new FriendRequest();
            var friendRequestTwo = new FriendRequest();
            try
            {
                friendRequestOne = _context.FriendRequests.Where(x => x.UserRequestId == userOneId).Where(x => x.UserResponderId == userTwoId).First();
            }
            catch (Exception)
            {

            }
            try
            {
                friendRequestTwo = _context.FriendRequests.Where(x => x.UserRequestId == userTwoId).Where(x => x.UserResponderId == userOneId).First();
            }
            catch (Exception)
            {

            }
            if (friendRequestOne == null && friendRequestTwo == null)
            {
                return BadRequest("Friend request with provided Ids does not exist");
            }
            if (friendRequestOne.IdfriendRequest != 0)
            {
                var friend = new Friend
                {
                    DateTime = DateTime.Now.AddHours(2),
                    UserRequestId = friendRequestOne.UserRequestId,
                    UserResponderId = friendRequestOne.UserResponderId
                };

                try
                {
                    _context.FriendRequests.Remove(friendRequestOne);
                    await _context.Friends.AddAsync(friend);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest("Error while trying to add friends");
                }
            }

            if (friendRequestTwo.IdfriendRequest != 0)
            {
                var friend = new Friend
                {
                    DateTime = DateTime.Now.AddHours(2),
                    UserRequestId = friendRequestTwo.UserRequestId,
                    UserResponderId = friendRequestTwo.UserResponderId
                };

                try
                {
                    _context.FriendRequests.Remove(friendRequestTwo);
                    await _context.Friends.AddAsync(friend);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest("Error while trying to add friends");
                }
            }

            return await Task.FromResult(Ok("Friends added successfully!"));
        }


        /// <summary>
        /// Removes friendship between two users
        /// </summary>
        /// <param name="userOneId">The userOneId<see cref="int"/>.</param>
        /// <param name="userTwoId">The userTwoId<see cref="int"/>.</param>
        /// <returns>.</returns>
        [HttpDelete("remove/{userOneId}/{userTwoId}")]
        public async Task<IActionResult> RemoveFriendsBasedOnIds([FromRoute] int userOneId, [FromRoute] int userTwoId)
        {
            if (_context.Users.Find(userOneId) == null)
            {
                return BadRequest("User for this userOneId does not exist");
            };
            if (_context.Users.Find(userTwoId) == null)
            {
                return BadRequest("User for this userTwoId does not exist");
            };

            var friendsCheckOne = new Friend();
            var friendsCheckTwo = new Friend();

            try
            {
                friendsCheckOne = await _context.Friends.Where(x => x.UserRequestId == userOneId).Where(x => x.UserResponderId == userTwoId).FirstAsync();
            }
            catch (Exception)
            {
                friendsCheckOne = null;
            }
            try
            {
                friendsCheckTwo = await _context.Friends.Where(x => x.UserRequestId == userTwoId).Where(x => x.UserResponderId == userOneId).FirstAsync();
            }
            catch (Exception)
            {
                friendsCheckTwo = null;
            }

            if(friendsCheckOne != null)
            {
                _context.Friends.Remove(friendsCheckOne);
                await _context.SaveChangesAsync();
                return Ok();
            }
            if(friendsCheckTwo != null)
            {
                _context.Friends.Remove(friendsCheckTwo);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest("Error happened while trying to remove friends");

        }


        /// <summary>
        /// Decline friend request based on friend request id.
        /// </summary>
        /// <param name="friendRequestId">User id.</param>
        /// <returns>.</returns>
        [HttpGet("decline/{friendRequestId}")]
        public async Task<ActionResult<List<FriendRequestDto>>> DeclineFriendRequest([FromRoute] int friendRequestId)
        {
            var friendRequest = _context.FriendRequests.Find(friendRequestId);
            if (friendRequest == null)
            {
                return BadRequest("Friend request with provided Id does not exist");
            }
            try
            {
                _context.FriendRequests.Remove(friendRequest);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Error while trying to decline friend request");
            }

            return await Task.FromResult(Ok("Friend request declined successfully!"));
        }

        /// <summary>
        /// Decline friend request based on user request id and user respond id.
        /// </summary>
        /// <param name="userOneId">The userOneId<see cref="int"/>.</param>
        /// <param name="userTwoId">The userTwoId<see cref="int"/>.</param>
        /// <returns>.</returns>
        [HttpGet("decline/{userOneId}/{userTwoId}")]
        public async Task<ActionResult<List<FriendRequestDto>>> DeclineFriendRequestBasedOnIds([FromRoute] int userOneId, [FromRoute] int userTwoId)
        {
            var friendRequestOne = new FriendRequest();
            var friendRequestTwo = new FriendRequest();
            try
            {
                friendRequestOne = _context.FriendRequests.Where(x => x.UserRequestId == userOneId).Where(x => x.UserResponderId == userTwoId).First();
            }
            catch (Exception)
            {

            }
            try
            {
                friendRequestTwo = _context.FriendRequests.Where(x => x.UserRequestId == userTwoId).Where(x => x.UserResponderId == userOneId).First();
            }
            catch (Exception)
            {

            }
            if (friendRequestOne == null && friendRequestTwo == null)
            {
                return BadRequest("Friend request with provided Ids does not exist");
            }
            if (friendRequestOne.IdfriendRequest != 0)
            {
                try
                {
                    _context.FriendRequests.Remove(friendRequestOne);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest("Error while trying to decline friend request");
                }
            }

            if (friendRequestTwo.IdfriendRequest != 0)
            {
                try
                {
                    _context.FriendRequests.Remove(friendRequestTwo);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest("Error while trying to decline friend request");
                }
            }

            return await Task.FromResult(Ok("Friend request declined successfully!"));
        }
    }
}
