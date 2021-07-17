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
    public class MessageController : ControllerBase
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
        public MessageController(PigeonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new message to send (You can reference users either with their Id's or their full names in the request body model properties)
        /// </summary>
        [HttpPost("{send}")]
        public ActionResult SendMessage([FromBody] SendMessageCommand model)
        {
            var allUsers = _context.Users.ToList();
            if (!string.IsNullOrEmpty(model.SenderFirstLastName) && !string.IsNullOrEmpty(model.ReceiverFirstLastname))
            {
                var allUsersNamesAndIds = new Dictionary<int, string>();
                foreach (var user in allUsers)
                {
                    string fullName = user.FirstName + ' ' + user.LastName;
                    allUsersNamesAndIds.Add(user.Iduser, fullName);
                }
                var userList = allUsersNamesAndIds.ToList();
                bool foundSender = false;
                bool foundReceiver = false;
                foreach (var item in userList)
                {
                    if (item.Value == model.SenderFirstLastName)
                    {
                        model.SenderId = item.Key;
                        foundSender = true;
                    }
                    if (item.Value == model.ReceiverFirstLastname)
                    {
                        model.ReceiverId = item.Key;
                        foundReceiver = true;
                    }
                }
                if (!foundSender)
                {
                    return NotFound("Could not find user with this sender full name");
                }
                if (!foundReceiver)
                {
                    return NotFound("Could not find user with this receiver full name");
                }
            }

            try
            {
                var userSender = _context.Users.Find(model.SenderId);
            }
            catch (Exception)
            {
                return NotFound("Could not find user with this sender id");
            }

            try
            {
                var userReceiver = _context.Users.Find(model.ReceiverId);
            }
            catch (Exception)
            {
                return NotFound("Could not find user with this receiver id");
            }

            if (string.IsNullOrEmpty(model.Text))
            {
                return BadRequest("Message text cannot be empty");
            }

            var message = new Message();
            message.DateTime = DateTime.Now.AddHours(2);
            message.Text = model.Text;
            message.SenderId = model.SenderId;
            message.ReceiverId = model.ReceiverId;


            try
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all messages between two users sorted by time they were sent
        /// </summary>
        [HttpGet("{firstUserID}/{secondUserID}")]
        public ActionResult<List<MessageDto>> GetMessages([FromRoute] int firstUserID, [FromRoute] int secondUserID)
        {
            var firstUser = _context.Users.Find(firstUserID);
            var secondUser = _context.Users.Find(secondUserID);
            if(firstUser == null)
            {
                return BadRequest("Could not find first user with this id");
            }

            if (secondUser == null)
            {
                return BadRequest("Could not find second user with this id");
            }

            var messages = new List<MessageDto>();
            var messages2 = new List<MessageDto>();
            try
            {
                var messagesFirstUser = _context.Messages.Where(x => x.SenderId == firstUserID).Where(x => x.ReceiverId == secondUserID).ToList();
                var messagesSecondUser = _context.Messages.Where(x => x.SenderId == secondUserID).Where(x => x.ReceiverId == firstUserID).ToList();
                foreach (var item in messagesFirstUser)
                {
                    var message = new MessageDto();
                    message.Text = item.Text;
                    message.IDMessage = item.Idmessage;
                    message.DateTime = item.DateTime;
                    message.SeenDateTime = item.SeenDateTime;
                    message.SenderId = item.SenderId;
                    message.ReceiverId = item.ReceiverId;
                    message.ReceiverFirstLastName = _context.Users.Find(item.ReceiverId).FirstName + ' ' + _context.Users.Find(item.ReceiverId).LastName;
                    message.SenderFirstLastName = _context.Users.Find(item.SenderId).FirstName + ' ' + _context.Users.Find(item.SenderId).LastName;
                    messages.Add(message);
                }
                foreach (var item in messagesSecondUser)
                {
                    var message = new MessageDto();
                    message.Text = item.Text;
                    message.IDMessage = item.Idmessage;
                    message.DateTime = item.DateTime;
                    message.SeenDateTime = item.SeenDateTime;
                    message.SenderId = item.SenderId;
                    message.ReceiverId = item.ReceiverId;
                    message.ReceiverFirstLastName = _context.Users.Find(item.ReceiverId).FirstName + ' ' + _context.Users.Find(item.ReceiverId).LastName;
                    message.SenderFirstLastName = _context.Users.Find(item.SenderId).FirstName + ' ' + _context.Users.Find(item.SenderId).LastName;
                    messages2.Add(message);
                }
            }
            catch (Exception)
            {
                return BadRequest("There are no messages between these two users");
            }
            List<MessageDto> messagesCombined = messages.Union(messages2).ToList();
            List<MessageDto> messagesDto = messagesCombined.OrderByDescending(x => x.DateTime).ToList();
            if(messagesDto.Count() == 0)
            {
                return BadRequest("There are no messages between these two users");
            }
            return Ok(messagesDto);
        }

        /// <summary>
        /// Get a list of users that messaged specified user and last message
        /// </summary>
        [HttpGet("users/{userID}")]
        public ActionResult<List<MessageUsersDto>> GetMessageUsers([FromRoute] int userID)
        {
            var userIDs = new List<int?>();
            var messagesbyuser = _context.Messages.Where(x => x.SenderId == userID).ToList();
            var messagesforuser = _context.Messages.Where(x => x.ReceiverId == userID).ToList();
            foreach (var item in messagesbyuser)
            {
                userIDs.Add(item.SenderId);
                userIDs.Add(item.ReceiverId);
            }

            foreach (var item in messagesforuser)
            {
                userIDs.Add(item.SenderId);
                userIDs.Add(item.ReceiverId);
            }

            userIDs.RemoveAll(x => x == userID);

            var finalUserIds = userIDs.Distinct().ToList();
            var usersDto = new List<MessageUsersDto>();

            var allMessages = messagesbyuser.Concat(messagesforuser).ToList().OrderByDescending(x => x.DateTime).ToList();




            foreach (var item in finalUserIds)
            {
                var lastSenderMessage = allMessages.Where(x => x.SenderId == item).FirstOrDefault();
                var lastReceiverMessage = allMessages.Where(x => x.ReceiverId == item).FirstOrDefault();

                var lastMessage = new Message();
                if(lastReceiverMessage != null && lastSenderMessage != null)
                {
                    lastMessage = lastReceiverMessage.DateTime > lastSenderMessage.DateTime ? lastReceiverMessage : lastSenderMessage;
                }
                if(lastReceiverMessage == null)
                {
                    lastMessage = lastSenderMessage;
                }
                if(lastSenderMessage == null)
                {
                    lastMessage = lastReceiverMessage;
                }

                var messageUser = new MessageUsersDto();
                var user = _context.Users.Find(item);
                messageUser.UserId = user.Iduser;
                messageUser.UserFirstLastname = user.FirstName + ' ' + user.LastName;
                messageUser.LastMessageText = lastMessage.Text;
                messageUser.LastMessageDateTime = lastMessage.DateTime;
                usersDto.Add(messageUser);
            }
            return Ok(usersDto.OrderByDescending(x => x.LastMessageDateTime).ToList());
        }

        /// <summary>
        /// Puts a seen datetime on specific message
        /// </summary>
        [HttpPost("{messageId}/seen")]
        public ActionResult SeenMessage([FromRoute] int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if(message == null)
            {
                return NotFound("Could not find message with this id");
            }

            message.SeenDateTime = DateTime.Now.AddHours(2);
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Puts a seen datetime on all messages between two users (Only works for )
        /// </summary>
        [HttpPost("{userReceiverId}/{userSenderId}/seen")]
        public ActionResult SeenMessage([FromRoute] int userReceiverId, [FromRoute] int userSenderId)
        {
            if (userReceiverId == null)
            {
                return BadRequest("Could not find receiver user with this id");
            }

            if (userSenderId == null)
            {
                return BadRequest("Could not find sender user with this id");
            }

            var messages = _context.Messages.Where(x => x.ReceiverId == userReceiverId).Where(x => x.SenderId == userSenderId).ToList();

            foreach (var item in messages)
            {
                if(item.SeenDateTime == null)
                {
                    item.SeenDateTime = DateTime.Now.AddHours(2);
                    _context.SaveChanges();
                }
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a specific message
        /// </summary>
        [HttpDelete("{messageId}")]
        public ActionResult DeleteMessage([FromRoute] int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if(message == null)
            {
                return NotFound("Message with this ID does not exist");
            }
            _context.Messages.Remove(message);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates a specific message text
        /// </summary>
        [HttpPut("{messageId}")]
        public ActionResult UpdateMessage([FromRoute] int messageId, [FromBody] UpdateMessageCommand model)
        {
            var message = _context.Messages.Find(messageId);
            if (message == null)
            {
                return NotFound("Message with this ID does not exist");
            }
            message.Text = model.Text;
            _context.SaveChanges();

            return Ok();
        }
    }
}
