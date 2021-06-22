namespace WebAPI.Controllers
{
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

    /// <summary>
    /// Defines the <see cref="UsersController" />.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Defines the _userRepository.
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Defines the _cityRepository.
        /// </summary>
        private ICityRepository _cityRepository;

        /// <summary>
        /// Defines the _context.
        /// </summary>
        private readonly PigeonContext _context;

        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Defines the _appSettings.
        /// </summary>
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="PigeonContext"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="appsettings">The appsettings<see cref="IOptions{AppSettings}"/>.</param>
        public UsersController(PigeonContext context, IMapper mapper, IOptions<AppSettings> appsettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appsettings.Value;
            _userRepository = new UserRepository(_context);
            _cityRepository = new CityRepository(_context);
        }

        /// <summary>
        /// Authenticate user by sending email and password in request body. If authorized this will return bearer header token property for further authorization
        /// </summary>
        /// <param name="model">The model<see cref="AuthenticateUserCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateUserCommand model)
        {
            var user = _userRepository.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            if (user.Verification==false)
                return BadRequest(new { message = "Your account hasn't been verified" });

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

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="model">The model<see cref="RegisterUserCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
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
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{UserDto}}}"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            IEnumerable<User> users = await _context.Users.Include(x => x.Gender).Include(x => x.UserType).Include(x => x.City).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        // GET: api/Users
        /// <summary>
        /// Finds all users that contain given string(specified in request body) in their first or last name
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{SearchUserDto}}}"/>.</returns>
        [HttpPost("search")]
        public async Task<ActionResult<List<SearchUserDto>>> FindUsers([FromBody] SearchUserCommand command)
        {
            string[] searchString = command.SearchString.Split(null);
            var firstPart = string.IsNullOrEmpty(searchString[0]) ? "" : searchString[0];
            var secondPart =  searchString.Length <= 1 ? "" : searchString[1];
            List<User> users = _context.Users.Include(x => x.Gender).Include(x => x.City).ToList();

            var usersOne = users.Where(x => x.FirstName.ToLower().StartsWith(firstPart.ToLower())).ToList();
            var usersThree = users.Where(x => x.LastName.ToLower().StartsWith(firstPart.ToLower())).ToList();
            var usersTwo = new List<User>();
            var usersFour = new List<User>();
            if (!string.IsNullOrEmpty(secondPart))
            {
                usersTwo = users.Where(x => x.FirstName.ToLower().StartsWith(secondPart.ToLower())).ToList();
                usersFour = users.Where(x => x.LastName.ToLower().StartsWith(secondPart.ToLower())).ToList();
            }
            var allUsers = usersOne.Union(usersTwo).Union(usersThree).Union(usersFour).ToList();
            var finalUsers = new List<SearchUserDto>();
            if (allUsers.Count() == 0)
            {
                return NotFound("No users found for this search string");
            }
            else
            {
                
                foreach (var user in allUsers)
                {
                    var usTemp = new SearchUserDto
                    {
                        City = _context.Cities.Find(user.CityId).Name,
                        Email = user.Email,
                        FirstLastName = user.FirstName + " " + user.LastName,
                        Gender = _context.Genders.Find(user.GenderId).Name,
                        IdUser = user.Iduser
                    };
                    finalUsers.Add(usTemp);
                }
                finalUsers.OrderBy(x => x.FirstLastName);
                return Ok(finalUsers);
            }
        }

        // GET: api/Users/5
        /// <summary>
        /// Gets one user
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{UserDto}}"/>.</returns>
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

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="model">The model<see cref="UpdateUserCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
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

        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="model">The model<see cref="UpdateUserPasswordCommand"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
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
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
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

        /// <summary>
        /// Verify user
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{UserDto}}"/>.</returns>
        [AllowAnonymous]
        [HttpGet("verify/{id}")]
        public async Task<ActionResult<UserDto>> VerifyUser([FromRoute] int id)
        {
            List<User> users = await _context.Users.Include(x => x.Gender).Include(x => x.UserType).Include(x => x.City).ToListAsync();
            var user = users.Find(x => x.Iduser == id);
            if (user == null || user.Verification==true)
            {
                return NotFound();
            }
            else
            {
                user.Verification = true;
                _context.Users.Update(user);
                _context.SaveChanges();
                user.PasswordHash = null;
                return await Task.FromResult(Ok(_mapper.Map<UserDto>(user)));
            }
        }

        /// <summary>
        /// The CreateUserWithGenderAndCityRegister.
        /// </summary>
        /// <param name="model">The model<see cref="RegisterUserCommand"/>.</param>
        /// <returns>The <see cref="User"/>.</returns>
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

        /// <summary>
        /// The CreateUserWithGenderAndCityUpdate.
        /// </summary>
        /// <param name="model">The model<see cref="UpdateUserCommand"/>.</param>
        /// <returns>The <see cref="User"/>.</returns>
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
