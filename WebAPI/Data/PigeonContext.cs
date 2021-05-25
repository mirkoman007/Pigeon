using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

#nullable disable

namespace WebAPI.Data
{
    public partial class PigeonContext : DbContext
    {
        public PigeonContext()
        {
        }

        public PigeonContext(DbContextOptions<PigeonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentReaction> CommentReactions { get; set; }
        public virtual DbSet<CommentReport> CommentReports { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DeletedMessage> DeletedMessages { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupReport> GroupReports { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageReaction> MessageReactions { get; set; }
        public virtual DbSet<MessageReport> MessageReports { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostReaction> PostReactions { get; set; }
        public virtual DbSet<PostReport> PostReports { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Warning> Warnings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Idcity)
                    .HasName("PK__City__36D350832428A874");

                entity.ToTable("City");

                entity.Property(e => e.Idcity).HasColumnName("IDCity");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__City__CountryID__062DE679");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Idcomment)
                    .HasName("PK__Comment__C71D7B98167B821A");

                entity.ToTable("Comment");

                entity.Property(e => e.Idcomment).HasColumnName("IDComment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.CommentNavigation)
                    .WithMany(p => p.InverseCommentNavigation)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__Comment__Comment__4BCC3ABA");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Comment__PostID__4AD81681");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__UserID__4CC05EF3");
            });

            modelBuilder.Entity<CommentReaction>(entity =>
            {
                entity.HasKey(e => e.IdcommentReaction)
                    .HasName("PK__CommentR__1DB6A590EB804A0C");

                entity.ToTable("CommentReaction");

                entity.Property(e => e.IdcommentReaction).HasColumnName("IDCommentReaction");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__CommentRe__Comme__4F9CCB9E");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__CommentRe__React__5090EFD7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__CommentRe__UserI__51851410");
            });

            modelBuilder.Entity<CommentReport>(entity =>
            {
                entity.HasKey(e => e.IdcommentReport)
                    .HasName("PK__CommentR__3027E215401566C0");

                entity.ToTable("CommentReport");

                entity.Property(e => e.IdcommentReport).HasColumnName("IDCommentReport");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__CommentRe__Comme__546180BB");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__CommentRe__Sende__5555A4F4");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Idcountry)
                    .HasName("PK__Country__D9D5A694A531E274");

                entity.ToTable("Country");

                entity.Property(e => e.Idcountry).HasColumnName("IDCountry");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeletedMessage>(entity =>
            {
                entity.HasKey(e => e.IddeletedMessage)
                    .HasName("PK__DeletedM__5D8807BA4A0FAACA");

                entity.ToTable("DeletedMessage");

                entity.Property(e => e.IddeletedMessage).HasColumnName("IDDeletedMessage");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.DeletedMessages)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__DeletedMe__Messa__2E3BD7D3");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.Idfriend)
                    .HasName("PK__Friend__FB73C390B5557795");

                entity.ToTable("Friend");

                entity.Property(e => e.Idfriend).HasColumnName("IDFriend");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserRequestId).HasColumnName("UserRequestID");

                entity.Property(e => e.UserResponderId).HasColumnName("UserResponderID");

                entity.HasOne(d => d.UserRequest)
                    .WithMany(p => p.FriendUserRequests)
                    .HasForeignKey(d => d.UserRequestId)
                    .HasConstraintName("FK__Friend__UserRequ__15702A09");

                entity.HasOne(d => d.UserResponder)
                    .WithMany(p => p.FriendUserResponders)
                    .HasForeignKey(d => d.UserResponderId)
                    .HasConstraintName("FK__Friend__UserResp__16644E42");
            });

            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.HasKey(e => e.IdfriendRequest)
                    .HasName("PK__FriendRe__FC78D63C12734EE8");

                entity.ToTable("FriendRequest");

                entity.Property(e => e.IdfriendRequest).HasColumnName("IDFriendRequest");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserRequestId).HasColumnName("UserRequestID");

                entity.Property(e => e.UserResponderId).HasColumnName("UserResponderID");

                entity.HasOne(d => d.UserRequest)
                    .WithMany(p => p.FriendRequestUserRequests)
                    .HasForeignKey(d => d.UserRequestId)
                    .HasConstraintName("FK__FriendReq__UserR__119F9925");

                entity.HasOne(d => d.UserResponder)
                    .WithMany(p => p.FriendRequestUserResponders)
                    .HasForeignKey(d => d.UserResponderId)
                    .HasConstraintName("FK__FriendReq__UserR__1293BD5E");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.Idgender)
                    .HasName("PK__Gender__42370E794E07D320");

                entity.ToTable("Gender");

                entity.Property(e => e.Idgender).HasColumnName("IDGender");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Idgroup)
                    .HasName("PK__Group__CB4260CABE1BC165");

                entity.ToTable("Group");

                entity.Property(e => e.Idgroup).HasColumnName("IDGroup");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GroupReport>(entity =>
            {
                entity.HasKey(e => e.IdgroupReport)
                    .HasName("PK__GroupRep__0A30267B81D1D28F");

                entity.ToTable("GroupReport");

                entity.Property(e => e.IdgroupReport).HasColumnName("IDGroupReport");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Reason)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserSenderId).HasColumnName("UserSenderID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupReports)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupRepo__Group__22CA2527");

                entity.HasOne(d => d.UserSender)
                    .WithMany(p => p.GroupReports)
                    .HasForeignKey(d => d.UserSenderId)
                    .HasConstraintName("FK__GroupRepo__UserS__23BE4960");
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasKey(e => e.Idmedia)
                    .HasName("PK__Media__F2B436C57C2D2BF5");

                entity.Property(e => e.Idmedia).HasColumnName("IDMedia");

                entity.Property(e => e.MediaPath)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Idmessage)
                    .HasName("PK__Message__195595EC87CD407D");

                entity.ToTable("Message");

                entity.Property(e => e.Idmessage).HasColumnName("IDMessage");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

                entity.Property(e => e.SeenDateTime).HasColumnType("datetime");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Message__Receive__2B5F6B28");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Message__SenderI__2A6B46EF");
            });

            modelBuilder.Entity<MessageReaction>(entity =>
            {
                entity.HasKey(e => e.IdmessageReaction)
                    .HasName("PK__MessageR__0AD165E7EC33E01D");

                entity.ToTable("MessageReaction");

                entity.Property(e => e.IdmessageReaction).HasColumnName("IDMessageReaction");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__MessageRe__Messa__33008CF0");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__MessageRe__React__33F4B129");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MessageRe__UserI__34E8D562");
            });

            modelBuilder.Entity<MessageReport>(entity =>
            {
                entity.HasKey(e => e.IdmessageReport)
                    .HasName("PK__MessageR__3D9BEF920E6265AD");

                entity.ToTable("MessageReport");

                entity.Property(e => e.IdmessageReport).HasColumnName("IDMessageReport");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Reason)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageReports)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__MessageRe__Messa__37C5420D");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__MessageRe__Sende__38B96646");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Idpost)
                    .HasName("PK__Post__8B0115BDE0A53C69");

                entity.ToTable("Post");

                entity.Property(e => e.Idpost).HasColumnName("IDPost");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Post__GroupID__3B95D2F1");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.MediaId)
                    .HasConstraintName("FK__Post__MediaID__61BB7BD9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__UserID__3C89F72A");
            });

            modelBuilder.Entity<PostReaction>(entity =>
            {
                entity.HasKey(e => e.IdpostReaction)
                    .HasName("PK__PostReac__EA97966A6EE56E39");

                entity.ToTable("PostReaction");

                entity.Property(e => e.IdpostReaction).HasColumnName("IDPostReaction");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostReact__PostI__46136164");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__PostReact__React__4707859D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostReact__UserI__47FBA9D6");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.HasKey(e => e.IdpostReport)
                    .HasName("PK__PostRepo__852C7D3A7DF3BE14");

                entity.ToTable("PostReport");

                entity.Property(e => e.IdpostReport).HasColumnName("IDpostReport");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Reason)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostRepor__PostI__4242D080");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__PostRepor__Sende__4336F4B9");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.HasKey(e => e.Idreaction)
                    .HasName("PK__Reaction__D866342A7EDC80D5");

                entity.ToTable("Reaction");

                entity.Property(e => e.Idreaction).HasColumnName("IDReaction");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__User__EAE6D9DF1DE80A3E");

                entity.ToTable("User");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).HasMaxLength(64);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__User__CityID__0EC32C7A");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__User__GenderID__0DCF0841");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK__User__UserTypeID__0CDAE408");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.IduserGroup)
                    .HasName("PK__UserGrou__2DE7BEFF8430C9AF");

                entity.ToTable("UserGroup");

                entity.Property(e => e.IduserGroup).HasColumnName("IDUserGroup");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__UserGroup__Group__1FEDB87C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserGroup__UserI__1EF99443");
            });

            modelBuilder.Entity<UserReport>(entity =>
            {
                entity.HasKey(e => e.IduserReport)
                    .HasName("PK__UserRepo__0867783A7D9768B5");

                entity.ToTable("UserReport");

                entity.Property(e => e.IduserReport).HasColumnName("IDUserReport");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserSenderId).HasColumnName("UserSenderID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserReportUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserRepor__UserI__1940BAED");

                entity.HasOne(d => d.UserSender)
                    .WithMany(p => p.UserReportUserSenders)
                    .HasForeignKey(d => d.UserSenderId)
                    .HasConstraintName("FK__UserRepor__UserS__1A34DF26");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.IduserType)
                    .HasName("PK__UserType__EA4074F26EB32BD0");

                entity.ToTable("UserType");

                entity.Property(e => e.IduserType).HasColumnName("IDUserType");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Warning>(entity =>
            {
                entity.HasKey(e => e.Idwarning)
                    .HasName("PK__Warning__EECE035159617911");

                entity.ToTable("Warning");

                entity.Property(e => e.Idwarning).HasColumnName("IDWarning");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.WarningAdmins)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__Warning__AdminID__278EDA44");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WarningUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Warning__UserID__269AB60B");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
