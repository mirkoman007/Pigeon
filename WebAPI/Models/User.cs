using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("User")]
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

        [Key]
        [Column("IDUser")]
        public int Iduser { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [MaxLength(64)]
        public byte[] PasswordHash { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDateTime { get; set; }
        public bool Verification { get; set; }
        [Column("UserTypeID")]
        public int UserTypeId { get; set; }
        [Column("GenderID")]
        public int GenderId { get; set; }
        [Column("CityID")]
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Users")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(GenderId))]
        [InverseProperty("Users")]
        public virtual Gender Gender { get; set; }
        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("Users")]
        public virtual UserType UserType { get; set; }
        [InverseProperty(nameof(CommentReaction.User))]
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        [InverseProperty(nameof(CommentReport.Sender))]
        public virtual ICollection<CommentReport> CommentReports { get; set; }
        [InverseProperty(nameof(Comment.User))]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty(nameof(FriendRequest.UserRequest))]
        public virtual ICollection<FriendRequest> FriendRequestUserRequests { get; set; }
        [InverseProperty(nameof(FriendRequest.UserResponder))]
        public virtual ICollection<FriendRequest> FriendRequestUserResponders { get; set; }
        [InverseProperty(nameof(Friend.UserRequest))]
        public virtual ICollection<Friend> FriendUserRequests { get; set; }
        [InverseProperty(nameof(Friend.UserResponder))]
        public virtual ICollection<Friend> FriendUserResponders { get; set; }
        [InverseProperty(nameof(GroupReport.UserSender))]
        public virtual ICollection<GroupReport> GroupReports { get; set; }
        [InverseProperty(nameof(MessageReaction.User))]
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        [InverseProperty(nameof(Message.Receiver))]
        public virtual ICollection<Message> MessageReceivers { get; set; }
        [InverseProperty(nameof(MessageReport.Sender))]
        public virtual ICollection<MessageReport> MessageReports { get; set; }
        [InverseProperty(nameof(Message.Sender))]
        public virtual ICollection<Message> MessageSenders { get; set; }
        [InverseProperty(nameof(PostReaction.User))]
        public virtual ICollection<PostReaction> PostReactions { get; set; }
        [InverseProperty(nameof(PostReport.Sender))]
        public virtual ICollection<PostReport> PostReports { get; set; }
        [InverseProperty(nameof(Post.User))]
        public virtual ICollection<Post> Posts { get; set; }
        [InverseProperty(nameof(UserGroup.User))]
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        [InverseProperty(nameof(UserReport.UserSender))]
        public virtual ICollection<UserReport> UserReportUserSenders { get; set; }
        [InverseProperty(nameof(UserReport.User))]
        public virtual ICollection<UserReport> UserReportUsers { get; set; }
        [InverseProperty(nameof(Warning.Admin))]
        public virtual ICollection<Warning> WarningAdmins { get; set; }
        [InverseProperty(nameof(Warning.User))]
        public virtual ICollection<Warning> WarningUsers { get; set; }
    }
}
