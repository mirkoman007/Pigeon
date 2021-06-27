using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Command;

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
        /// Creates a new group (WORK IN PROGRESS, need to somehow connect users to group)
        /// </summary>
        [HttpPost]
        public IActionResult CreateGroup([FromBody] CreateGroupCommand model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Name property cannot be null");
            }
            var group = _mapper.Map<Group>(model);
            group.DateTime = DateTime.Now.AddHours(2);

            try
            {
                _context.Groups.Add(group);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
