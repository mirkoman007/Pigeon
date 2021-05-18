using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Command;
using WebAPI.Models.DTO;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly PigeonContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(PigeonContext context, IMapper mapper, IOptions<AppSettings> appsettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appsettings.Value;
            _userRepository = new UserRepository(_context);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateUserCommand model)
        {
            var user = _userRepository.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Iduser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new UserDto
            {
                IdUser = user.Iduser,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                CreateDateTime = user.CreateDateTime,
                GenderID = user.GenderId,
                CityID = user.CityId,
                UserTypeID = user.UserTypeId,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserCommand model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userRepository.Create(user, model.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            IEnumerable<User> users = await _context.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            user.PasswordHash = null;
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<UserDto>(user));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateUserCommand model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Iduser = id;

            try
            {
                // update user 
                _userRepository.Update(user, model.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
