using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Command;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
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
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="PigeonContext"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public GroupController(PigeonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new group and adds specified user as a group admin
        /// </summary>
        [HttpPost]
        public ActionResult<GroupDto> CreateGroup([FromBody] CreateGroupCommand model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Name property cannot be null");
            }
            var group = _mapper.Map<Group>(model);
            group.DateTime = DateTime.Now.AddHours(2);

            var user = _context.Users.Find(model.CreatorUserId);
            if(user == null)
            {
                return NotFound("User with provided CreatorUserId does not exist");
            }


            try
            {
                _context.Groups.Add(group);
                var userGroup = new UserGroup();
                userGroup.UserType = 1;
                userGroup.UserId = user.Iduser;


                _context.SaveChanges();
                userGroup.GroupId = group.Idgroup;
                _context.UserGroups.Add(userGroup);
                _context.SaveChanges();

                var groupDto = new GroupDto();
                groupDto.IDGroup = group.Idgroup;
                groupDto.Name = group.Name;
                groupDto.Decription = group.Description;
                groupDto.CreatedDateTime = group.DateTime;
                return Ok(groupDto);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates specified group
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult UpdateGroup([FromBody] CreateGroupCommand model, [FromRoute] int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound("Could not find group with this id");
            }
            try
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    group.Name = model.Name;
                }
                if (!string.IsNullOrEmpty(model.Description))
                {
                    group.Description = model.Description;
                }
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                NotFound("Group with this id does not exist");
            }
            return BadRequest("An error occured");

        }

        /// <summary>
        /// Get all groups
        /// </summary>
        [HttpGet]
        public ActionResult<List<GroupDto>> GetAllGroups()
        {
            var allGroups = _context.Groups.ToList();
            var allGroupsDto = new List<GroupDto>();
            foreach (var group in allGroups)
            {
                var groupDto = new GroupDto();
                groupDto.IDGroup = group.Idgroup;
                groupDto.Name = group.Name;
                groupDto.Decription = group.Description;
                groupDto.CreatedDateTime = group.DateTime;
                allGroupsDto.Add(groupDto);
            }

            return Ok(allGroupsDto);
        }

        /// <summary>
        /// Get specified group
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<GroupDto> GetGroup([FromRoute] int id)
        {
            var group = _context.Groups.Find(id);
            if(group == null)
            {
                return NotFound("Could not find group with this id");
            }
            var groupDto = new GroupDto();
            groupDto.IDGroup = group.Idgroup;
            groupDto.Name = group.Name;
            groupDto.Decription = group.Description;
            groupDto.CreatedDateTime = group.DateTime;
            return Ok(group);
        }

        /// <summary>
        /// Deletes specified group
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult DeleteGroup([FromRoute] int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound("Could not find group with this id");
            }
            var userGroups = _context.UserGroups.Where(x => x.GroupId == id).ToList();
            foreach (var userGroup in userGroups)
            {
                _context.UserGroups.Remove(userGroup);
            }

            _context.Groups.Remove(group);
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Get specified group admins
        /// </summary>
        [HttpGet("{id}/admins")]
        public ActionResult<GroupUserDto> GetGroupAdmins([FromRoute] int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound("Could not find group with this id");
            }
            var admins = _context.UserGroups.Where(x => x.UserType == 1).Where(y=>y.GroupId == id).ToList();
            var adminsDto = new List<GroupUserDto>();
            foreach (var admin in admins)
            {
                var adminDto = new GroupUserDto();
                adminDto.IdUser = admin.UserId;
                adminDto.FirstLastname = _context.Users.Find(admin.UserId).FirstName + ' ' + _context.Users.Find(admin.UserId).LastName;
                adminsDto.Add(adminDto);
            }

            return Ok(adminsDto);
        }

        /// <summary>
        /// Get specified group users
        /// </summary>
        [HttpGet("{id}/users")]
        public ActionResult<GroupUserDto> GetGroupUsers([FromRoute] int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound("Could not find group with this id");
            }
            var admins = _context.UserGroups.Where(x => x.UserType == 2).Where(y => y.GroupId == id).ToList();
            var adminsDto = new List<GroupUserDto>();
            foreach (var admin in admins)
            {
                var adminDto = new GroupUserDto();
                adminDto.IdUser = admin.UserId;
                adminDto.FirstLastname = _context.Users.Find(admin.UserId).FirstName + ' ' + _context.Users.Find(admin.UserId).LastName;
                adminsDto.Add(adminDto);
            }

            return Ok(adminsDto);
        }

        /// <summary>
        /// Get specified group all members (admins+users)
        /// </summary>
        [HttpGet("{id}/members")]
        public ActionResult<GroupUserDto> GetGroupMembers([FromRoute] int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound("Could not find group with this id");
            }
            var admins = _context.UserGroups.Where(y => y.GroupId == id).ToList();
            var adminsDto = new List<GroupUserDto>();
            foreach (var admin in admins)
            {
                var adminDto = new GroupUserDto();
                adminDto.IdUser = admin.UserId;
                adminDto.FirstLastname = _context.Users.Find(admin.UserId).FirstName + ' ' + _context.Users.Find(admin.UserId).LastName;
                adminsDto.Add(adminDto);
            }

            return Ok(adminsDto);
        }

        /// <summary>
        /// Adds new user to group (Possible UserType property values are "user" and "admin")
        /// </summary>
        [HttpPost("add")]
        public IActionResult AddUserToGroup([FromBody] AddUserGroupCommand model)
        {
            if (string.IsNullOrEmpty(model.UserType))
            {
                return BadRequest("UserType property cannot be null (It has to be either user or admin)");
            }
            var userGroup = new UserGroup();
            if (_context.Groups.Find(model.GroupId) == null)
            {
                return BadRequest("Group with provided id does not exist");
            }

            if (model.UserType.ToLower().Equals("admin"))
            {
                userGroup.UserType = 1;
            }
            else if (model.UserType.ToLower().Equals("user"))
            {
                userGroup.UserType = 2;
            }
            else
            {
                return BadRequest("Invalid UserType property. Please put property value to admin or user");
            }
            userGroup.UserId = model.UserId;
            userGroup.GroupId = model.GroupId;

            var users = _context.Users.ToList();
            if (!string.IsNullOrEmpty(model.UserFirstLastname))
            {
                foreach (var user in users)
                {
                    if(model.UserFirstLastname == (user.FirstName + ' ' + user.LastName))
                    {
                        userGroup.UserId = user.Iduser;
                    }
                }
            }

            try
            {
                _context.UserGroups.Add(userGroup);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates specified role of a user in a group (for example admin to user )
        /// </summary>
        [HttpPut("update")]
        public IActionResult UpdateUserInGroup([FromBody] AddUserGroupCommand model)
        {
            if (string.IsNullOrEmpty(model.UserType))
            {
                return BadRequest("UserType property cannot be null (It has to be either user or admin)");
            }
            var userGroup = new UserGroup();
            if (_context.Users.Find(model.UserId) == null)
            {
                return BadRequest("User with provided id does not exist");
            }
            if (_context.Groups.Find(model.GroupId) == null)
            {
                return BadRequest("Group with provided id does not exist");
            }

            if (model.UserType.ToLower().Equals("admin"))
            {
                userGroup.UserType = 1;
            }
            else if (model.UserType.ToLower().Equals("user"))
            {
                userGroup.UserType = 2;
            }
            else
            {
                return BadRequest("Invalid UserType property. Please put property value to admin or user");
            }
            userGroup.UserId = model.UserId;
            userGroup.GroupId = model.GroupId;


            try
            {
                var existingUser = _context.UserGroups.Where(x => x.UserId == model.UserId).Where(y => y.GroupId == model.GroupId).FirstOrDefault();
                existingUser.UserType = userGroup.UserType;
                
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Removes user from group
        /// </summary>
        [HttpDelete("remove/{userId}/{groupId}")]
        public IActionResult RemoveUserFromGroup([FromRoute] int userId, [FromRoute] int groupId)
        {
            var userGroup = new UserGroup();
            if (_context.Users.Find(userId) == null)
            {
                return NotFound("User with provided id does not exist");
            }
            if (_context.Groups.Find(groupId) == null)
            {
                return NotFound("Group with provided id does not exist");
            }

            try
            {
                userGroup = _context.UserGroups.Where(x => x.UserId == userId).Where(y => y.GroupId == groupId).FirstOrDefault();
                try
                {
                    _context.UserGroups.Remove(userGroup);
                    _context.SaveChanges();
                    return Ok();
                }
                catch (ApplicationException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            catch (Exception)
            {
                return NotFound("Specified user is not a member of this group");
            }
        }

    }
}
