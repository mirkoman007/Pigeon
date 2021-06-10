using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private ICityRepository _cityRepository;
        private readonly PigeonContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(PigeonContext context, IMapper mapper, IOptions<AppSettings> appsettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appsettings.Value;
            _userRepository = new UserRepository(_context);
            _cityRepository = new CityRepository(_context);
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
                Gender = _context.Genders.Find(user.GenderId).Name,
                City = _context.Cities.Find(user.CityId).Name,
                UserType = _context.UserTypes.Find(user.UserTypeId).Value,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserCommand model)
        {
            // map model to entity
            var user = CreateUserWithGenderAndCityRegister(model);
            user.Verification = false;

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
            IEnumerable<User> users = await _context.Users.Include(x => x.Gender).Include(x => x.UserType).Include(x => x.City).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            List<User> users = await _context.Users.Include(x => x.Gender).Include(x => x.UserType).Include(x => x.City).ToListAsync();
            var user = users.Find(x => x.Iduser == id);
            user.PasswordHash = null;
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return await Task.FromResult(Ok(_mapper.Map<UserDto>(user)));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateUserCommand model)
        {
            // map model to entity and set id
            var user = CreateUserWithGenderAndCityUpdate(model);
            user.Iduser = id;

            try
            {
                // update user 
                _userRepository.Update(user);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/password")]
        public IActionResult UpdatePassword(int id, [FromBody] UpdateUserPasswordCommand model)
        {
            // map model to entity and set id
            var user = new User();
            user.Iduser = id;

            try
            {
                if (!string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrEmpty(model.OldPassword))
                {
                    // update user 
                    _userRepository.Update(user, model.Password, model.OldPassword);
                    return Ok();
                }
                else
                {
                    return BadRequest("There was a problem with passwords(Check if null or property name correctly spelled)");
                }
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

        [HttpGet("verify/{id}")]
        public async Task<ActionResult<UserDto>> VerifyUser([FromRoute] int id)
        {
            List<User> users = await _context.Users.Include(x => x.Gender).Include(x => x.UserType).Include(x => x.City).ToListAsync();
            var user = users.Find(x => x.Iduser == id);
            user.Verification = true;
            _context.Users.Update(user);
            _context.SaveChanges();
            user.PasswordHash = null;
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return await Task.FromResult(Ok(_mapper.Map<UserDto>(user)));
            }
        }

        private User CreateUserWithGenderAndCityRegister(RegisterUserCommand model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Birthday = model.Birthday,
                CreateDateTime = DateTime.Now,
                UserTypeId = 2,
                Verification = false
            };
            if (!_context.Cities.Any(a => a.Name == model.City))
            {
                City city = new City { Name = model.City, CountryId = 1 };
                _context.Cities.Add(city);
                _context.SaveChanges();
                user.CityId = _context.Cities.SingleOrDefaultAsync(c => c.Name == city.Name).Result.Idcity;
            }
            else
            {
                user.CityId = _context.Cities.SingleOrDefaultAsync(c => c.Name == model.City).Result.Idcity;
            }

            if (model.Gender.ToLower().Equals("male"))
            {
                user.GenderId = 1;
            }
            else
            {
                user.GenderId = 2;
            }
            user.UserTypeId = 2;
            return user;
        }
        private User CreateUserWithGenderAndCityUpdate(UpdateUserCommand model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Birthday = model.Birthday,
                UserTypeId = 2
            };
            if (!string.IsNullOrEmpty(model.City))
            {
                if (!_context.Cities.Any(a => a.Name == model.City))
                {
                    City city = new City { Name = model.City, CountryId = 1 };
                    _context.Cities.Add(city);
                    _context.SaveChanges();
                    user.CityId = _context.Cities.SingleOrDefaultAsync(c => c.Name == city.Name).Result.Idcity;
                }
                else
                {
                    user.CityId = _context.Cities.SingleOrDefaultAsync(c => c.Name == model.City).Result.Idcity;
                }
            }

            if (!string.IsNullOrEmpty(model.Gender))
            {
                if (model.Gender.ToLower().Equals("male"))
                {
                    user.GenderId = 1;
                }
                else
                {
                    user.GenderId = 2;
                }
            }
            user.UserTypeId = 2;
            return user;
        }

    }
}
