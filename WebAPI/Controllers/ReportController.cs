using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ReportController : ControllerBase
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
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="PigeonContext"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public ReportController(PigeonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Comment report (Put CommentId value into ReportObjectId property of request body model)
        /// </summary>
        [HttpPost("comment")]
        public ActionResult ReportComment([FromBody] ReportCommand model)
        {
            if (_context.Users.Find(model.SenderId) == null)
            {
                return NotFound("Could not find sender user with this id");
            }

            if (_context.Comments.Find(model.ReportObjectId) == null)
            {
                return NotFound("Could not find comment with this id");
            }

            var commentReport = new CommentReport();
            commentReport.Reason = model.Reason;
            commentReport.Explanation = model.Explanation;
            commentReport.DateTime = DateTime.Now.AddHours(2);
            commentReport.CommentId = model.ReportObjectId;
            commentReport.SenderId = model.SenderId;

            try
            {
                _context.CommentReports.Add(commentReport);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to add comment report.");
            }
        }

        /// <summary>
        /// Get all comment reports
        /// </summary>
        [HttpGet("comment")]
        public ActionResult<List<ObjectReportDto>> GetCommentReports()
        {
            var commentReports = _context.CommentReports.ToList();
            var commentReportsDto = new List<ObjectReportDto>();
            foreach (var comment in commentReports)
            {
                var commentDto = new ObjectReportDto();
                commentDto.Id = comment.IdcommentReport;
                commentDto.Reason = comment.Reason;
                commentDto.Explanation = comment.Explanation;
                commentDto.DateTime = comment.DateTime;
                commentDto.ObjectReportId = comment.CommentId;
                commentDto.SenderId = comment.SenderId;
                commentDto.SenderFirstLastName = _context.Users.Find(comment.SenderId).FirstName + ' ' +
                    _context.Users.Find(comment.SenderId).LastName;
                commentReportsDto.Add(commentDto);
            }
            return Ok(commentReportsDto);
        }

        /// <summary>
        /// Message report (Put MessageId value into ReportObjectId property of request body model)
        /// </summary>
        [HttpPost("message")]
        public ActionResult ReportMessage([FromBody] ReportCommand model)
        {
            if (_context.Users.Find(model.SenderId) == null)
            {
                return NotFound("Could not find sender user with this id");
            }

            if (_context.Messages.Find(model.ReportObjectId) == null)
            {
                return NotFound("Could not find message with this id");
            }

            var messageReport = new MessageReport();
            messageReport.Reason = model.Reason;
            messageReport.Explanation = model.Explanation;
            messageReport.DateTime = DateTime.Now.AddHours(2);
            messageReport.MessageId = model.ReportObjectId;
            messageReport.SenderId = model.SenderId;

            try
            {
                _context.MessageReports.Add(messageReport);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to add message report.");
            }
        }

        /// <summary>
        /// Get all message reports
        /// </summary>
        [HttpGet("message")]
        public ActionResult<List<ObjectReportDto>> GetMessageReports()
        {
            var commentReports = _context.MessageReports.ToList();
            var commentReportsDto = new List<ObjectReportDto>();
            foreach (var comment in commentReports)
            {
                var commentDto = new ObjectReportDto();
                commentDto.Id = comment.IdmessageReport;
                commentDto.Reason = comment.Reason;
                commentDto.Explanation = comment.Explanation;
                commentDto.DateTime = comment.DateTime;
                commentDto.ObjectReportId = comment.MessageId;
                commentDto.SenderId = comment.SenderId;
                commentDto.SenderFirstLastName = _context.Users.Find(comment.SenderId).FirstName + ' ' +
                    _context.Users.Find(comment.SenderId).LastName;
                commentReportsDto.Add(commentDto);
            }
            return Ok(commentReportsDto);
        }

        /// <summary>
        /// Post report (Put PostId value into ReportObjectId property of request body model)
        /// </summary>
        [HttpPost("post")]
        public ActionResult ReportPost([FromBody] ReportCommand model)
        {
            if (_context.Users.Find(model.SenderId) == null)
            {
                return NotFound("Could not find sender user with this id");
            }

            if (_context.Posts.Find(model.ReportObjectId) == null)
            {
                return NotFound("Could not find post with this id");
            }

            var postReport = new PostReport();
            postReport.Reason = model.Reason;
            postReport.Explanation = model.Explanation;
            postReport.DateTime = DateTime.Now.AddHours(2);
            postReport.PostId = model.ReportObjectId;
            postReport.SenderId = model.SenderId;

            try
            {
                _context.PostReports.Add(postReport);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to add post report.");
            }
        }

        /// <summary>
        /// Get all message reports
        /// </summary>
        [HttpGet("post")]
        public ActionResult<List<ObjectReportDto>> GetPostReports()
        {
            var commentReports = _context.PostReports.ToList();
            var commentReportsDto = new List<ObjectReportDto>();
            foreach (var comment in commentReports)
            {
                var commentDto = new ObjectReportDto();
                commentDto.Id = comment.IdpostReport;
                commentDto.Reason = comment.Reason;
                commentDto.Explanation = comment.Explanation;
                commentDto.DateTime = comment.DateTime;
                commentDto.ObjectReportId = comment.PostId;
                commentDto.SenderId = comment.SenderId;
                commentDto.SenderFirstLastName = _context.Users.Find(comment.SenderId).FirstName + ' ' +
                    _context.Users.Find(comment.SenderId).LastName;
                commentReportsDto.Add(commentDto);
            }
            return Ok(commentReportsDto);
        }

        /// <summary>
        /// Group report (Put GroupId value into ReportObjectId property of request body model)
        /// </summary>
        [HttpPost("group")]
        public ActionResult ReportGroup([FromBody] ReportCommand model)
        {
            if (_context.Users.Find(model.SenderId) == null)
            {
                return NotFound("Could not find sender user with this id");
            }

            if (_context.Groups.Find(model.ReportObjectId) == null)
            {
                return NotFound("Could not find group with this id");
            }

            var groupReport = new GroupReport();
            groupReport.Reason = model.Reason;
            groupReport.Explanation = model.Explanation;
            groupReport.DateTime = DateTime.Now.AddHours(2);
            groupReport.GroupId = model.ReportObjectId;
            groupReport.UserSenderId = model.SenderId;
            groupReport.Approved = false;

            try
            {
                _context.GroupReports.Add(groupReport);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to add group report.");
            }
        }

        /// <summary>
        /// Get all message reports
        /// </summary>
        [HttpGet("group")]
        public ActionResult<List<ApproveObjectReportDto>> GetGroupReports()
        {
            var groupReports = _context.GroupReports.ToList();
            var groupReportsDto = new List<ApproveObjectReportDto>();
            
            foreach (var group in groupReports)
            {
                var groupDto = new ApproveObjectReportDto();
                groupDto.Id = group.IdgroupReport;
                groupDto.Reason = group.Reason;
                groupDto.Explanation = group.Explanation;
                groupDto.DateTime = group.DateTime;
                groupDto.ObjectReportId = group.GroupId;
                groupDto.SenderId = group.UserSenderId;
                groupDto.SenderFirstLastName = _context.Users.Find(group.UserSenderId).FirstName + ' ' +
                    _context.Users.Find(group.UserSenderId).LastName;
                groupDto.Approved = group.Approved;
                groupReportsDto.Add(groupDto);
            }
            return Ok(groupReportsDto);
        }

        /// <summary>
        /// User report (Put UserId value into ReportObjectId property of request body model)
        /// </summary>
        [HttpPost("user")]
        public ActionResult UserGroup([FromBody] ReportCommand model)
        {
            if (_context.Users.Find(model.SenderId) == null || _context.Users.Find(model.ReportObjectId) == null)
            {
                return NotFound("Could not find user with this id");
            }

            var userReport = new UserReport();
            userReport.Reason = model.Reason;
            userReport.Explanation = model.Explanation;
            userReport.DateTime = DateTime.Now.AddHours(2);
            userReport.UserId = model.ReportObjectId;
            userReport.UserSenderId = model.SenderId;
            userReport.Approved = false;

            try
            {
                _context.UserReports.Add(userReport);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to add user report.");
            }
        }

        /// <summary>
        /// Get all user reports
        /// </summary>
        [HttpGet("user")]
        public ActionResult<List<ApproveObjectReportDto>> GetUserReports()
        {
            var groupReports = _context.UserReports.ToList();
            var groupReportsDto = new List<ApproveObjectReportDto>();

            foreach (var group in groupReports)
            {
                var groupDto = new ApproveObjectReportDto();
                groupDto.Id = group.IduserReport;
                groupDto.Reason = group.Reason;
                groupDto.Explanation = group.Explanation;
                groupDto.DateTime = group.DateTime;
                groupDto.ObjectReportId = group.UserId;
                groupDto.SenderId = group.UserSenderId;
                groupDto.SenderFirstLastName = _context.Users.Find(group.UserSenderId).FirstName + ' ' +
                    _context.Users.Find(group.UserSenderId).LastName;
                groupDto.Approved = group.Approved;
                groupReportsDto.Add(groupDto);
            }
            return Ok(groupReportsDto);
        }

        /// <summary>
        /// Give warning
        /// </summary>
        [HttpPost("warning")]
        public ActionResult GiveWarning([FromBody] WarningCommand model)
        {
            if (_context.Users.Find(model.UserId) == null || _context.Users.Find(model.AdminId) == null)
            {
                return NotFound("Could not find user with this id");
            }

            var warning = new Warning();
            warning.Reason = model.Reason;
            warning.Explanation = model.Explanation;
            warning.DateTime = DateTime.Now.AddHours(2);
            warning.UserId = model.UserId;
            warning.AdminId = model.AdminId;

            try
            {
                _context.Warnings.Add(warning);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to add warning.");
            }
        }

        /// <summary>
        /// Get all user warning
        /// </summary>
        [HttpGet("warning/user/{userId}")]
        public ActionResult<List<WarningDto>> GetUserWarnings([FromRoute] int userId)
        {
            var warnings = _context.Warnings.Where(x =>x.UserId == userId).ToList();
            var warningsDto = new List<WarningDto>();

            foreach (var warning in warningsDto)
            {
                var warningDto = new WarningDto();
                warningDto.Id = warning.Id;
                warningDto.Reason = warning.Reason;
                warningDto.Explanation = warning.Explanation;
                warningDto.DateTime = warning.DateTime;
                warningDto.UserId = warning.UserId;
                warningDto.AdminId = warning.AdminId;
                warningDto.UserFirstLastName = _context.Users.Find(warning.UserId).FirstName + ' ' +
                    _context.Users.Find(warning.UserId).LastName;
                warningDto.AdminFirstLastName = _context.Users.Find(warning.AdminId).FirstName + ' ' +
                _context.Users.Find(warning.AdminId).LastName;
                warningsDto.Add(warningDto);
            }
            return Ok(warningsDto);
        }

    }
}
