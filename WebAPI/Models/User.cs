using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class User
    {
        public User()
        {
            CommentReactions = new HashSet<CommentReaction>();
            CommentReports = new HashSet<CommentReport>();
            Comments = new HashSet<Comment>();
            FriendRequestUserRequests = new HashSet<FriendRequest>();
            FriendRequestUserResponders = new HashSet<FriendRequest>();
            FriendUserRequests = new HashSet<Friend>();
            FriendUserResponders = new HashSet<Friend>();
            GroupReports = new HashSet<GroupReport>();
            MessageReactions = new HashSet<MessageReaction>();
            MessageReceivers = new HashSet<Message>();
            MessageReports = new HashSet<MessageReport>();
            MessageSenders = new HashSet<Message>();
            PostReactions = new HashSet<PostReaction>();
            PostReports = new HashSet<PostReport>();
            Posts = new HashSet<Post>();
            UserGroups = new HashSet<UserGroup>();
            UserReportUserSenders = new HashSet<UserReport>();
            UserReportUsers = new HashSet<UserReport>();
            WarningAdmins = new HashSet<Warning>();
            WarningUsers = new HashSet<Warning>();
        }

        public int Iduser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public bool? Verification { get; set; }
        public int? UserTypeId { get; set; }
        public int? GenderId { get; set; }
        public int? LocationId { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Location Location { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        public virtual ICollection<CommentReport> CommentReports { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestUserRequests { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestUserResponders { get; set; }
        public virtual ICollection<Friend> FriendUserRequests { get; set; }
        public virtual ICollection<Friend> FriendUserResponders { get; set; }
        public virtual ICollection<GroupReport> GroupReports { get; set; }
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<MessageReport> MessageReports { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserReport> UserReportUserSenders { get; set; }
        public virtual ICollection<UserReport> UserReportUsers { get; set; }
        public virtual ICollection<Warning> WarningAdmins { get; set; }
        public virtual ICollection<Warning> WarningUsers { get; set; }
    }
}
