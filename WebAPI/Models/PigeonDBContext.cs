using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI.Models
{
    public partial class PigeonDBContext : DbContext
    {
        public PigeonDBContext()
        {
        }

        public PigeonDBContext(DbContextOptions<PigeonDBContext> options)
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
        public virtual DbSet<Location> Locations { get; set; }
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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-FRANJO\\SQLEXPRESS;Database=PigeonDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Idcity)
                    .HasName("PK__City__36D35083060BD5BF");

                entity.ToTable("City");

                entity.Property(e => e.Idcity).HasColumnName("IDCity");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__City__CountryID__38996AB5");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Idcomment)
                    .HasName("PK__Comment__C71D7B98DC902D17");

                entity.ToTable("Comment");

                entity.Property(e => e.Idcomment).HasColumnName("IDComment");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Comment__PostID__00200768");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__UserID__01142BA1");
            });

            modelBuilder.Entity<CommentReaction>(entity =>
            {
                entity.HasKey(e => e.IdcommentReaction)
                    .HasName("PK__CommentR__1DB6A590256F2D8B");

                entity.ToTable("CommentReaction");

                entity.Property(e => e.IdcommentReaction).HasColumnName("IDCommentReaction");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__CommentRe__Comme__03F0984C");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__CommentRe__React__04E4BC85");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__CommentRe__UserI__05D8E0BE");
            });

            modelBuilder.Entity<CommentReport>(entity =>
            {
                entity.HasKey(e => e.IdcommentReport)
                    .HasName("PK__CommentR__3027E2152A719714");

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
                    .HasConstraintName("FK__CommentRe__Comme__08B54D69");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__CommentRe__Sende__09A971A2");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Idcountry)
                    .HasName("PK__Country__D9D5A694771E0B8E");

                entity.ToTable("Country");

                entity.Property(e => e.Idcountry).HasColumnName("IDCountry");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeletedMessage>(entity =>
            {
                entity.HasKey(e => e.IddeletedMessage)
                    .HasName("PK__DeletedM__5D8807BA53765123");

                entity.ToTable("DeletedMessage");

                entity.Property(e => e.IddeletedMessage).HasColumnName("IDDeletedMessage");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.DeletedMessages)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__DeletedMe__Messa__6477ECF3");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.Idfriend)
                    .HasName("PK__Friend__FB73C390BB07B0CD");

                entity.ToTable("Friend");

                entity.Property(e => e.Idfriend).HasColumnName("IDFriend");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserRequestId).HasColumnName("UserRequestID");

                entity.Property(e => e.UserResponderId).HasColumnName("UserResponderID");

                entity.HasOne(d => d.UserRequest)
                    .WithMany(p => p.FriendUserRequests)
                    .HasForeignKey(d => d.UserRequestId)
                    .HasConstraintName("FK__Friend__UserRequ__4BAC3F29");

                entity.HasOne(d => d.UserResponder)
                    .WithMany(p => p.FriendUserResponders)
                    .HasForeignKey(d => d.UserResponderId)
                    .HasConstraintName("FK__Friend__UserResp__4CA06362");
            });

            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.HasKey(e => e.IdfriendRequest)
                    .HasName("PK__FriendRe__FC78D63C2FF6AEE8");

                entity.ToTable("FriendRequest");

                entity.Property(e => e.IdfriendRequest).HasColumnName("IDFriendRequest");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserRequestId).HasColumnName("UserRequestID");

                entity.Property(e => e.UserResponderId).HasColumnName("UserResponderID");

                entity.HasOne(d => d.UserRequest)
                    .WithMany(p => p.FriendRequestUserRequests)
                    .HasForeignKey(d => d.UserRequestId)
                    .HasConstraintName("FK__FriendReq__UserR__47DBAE45");

                entity.HasOne(d => d.UserResponder)
                    .WithMany(p => p.FriendRequestUserResponders)
                    .HasForeignKey(d => d.UserResponderId)
                    .HasConstraintName("FK__FriendReq__UserR__48CFD27E");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.Idgender)
                    .HasName("PK__Gender__42370E7906DC3637");

                entity.ToTable("Gender");

                entity.Property(e => e.Idgender).HasColumnName("IDGender");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Idgroup)
                    .HasName("PK__Group__CB4260CAF0A0F932");

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
                    .HasName("PK__GroupRep__0A30267BC1617DF0");

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
                    .HasConstraintName("FK__GroupRepo__Group__59063A47");

                entity.HasOne(d => d.UserSender)
                    .WithMany(p => p.GroupReports)
                    .HasForeignKey(d => d.UserSenderId)
                    .HasConstraintName("FK__GroupRepo__UserS__59FA5E80");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Idlocation)
                    .HasName("PK__Location__C2B752777C28FFA3");

                entity.ToTable("Location");

                entity.Property(e => e.Idlocation).HasColumnName("IDLocation");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Location__CityID__3B75D760");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Location__Countr__3C69FB99");
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasKey(e => e.Idmedia)
                    .HasName("PK__Media__F2B436C551B6925C");

                entity.Property(e => e.Idmedia).HasColumnName("IDMedia");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Media__PostID__74AE54BC");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Idmessage)
                    .HasName("PK__Message__195595ECBBD20A5B");

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
                    .HasConstraintName("FK__Message__Receive__619B8048");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Message__SenderI__60A75C0F");
            });

            modelBuilder.Entity<MessageReaction>(entity =>
            {
                entity.HasKey(e => e.IdmessageReaction)
                    .HasName("PK__MessageR__0AD165E7BDEBD3FC");

                entity.ToTable("MessageReaction");

                entity.Property(e => e.IdmessageReaction).HasColumnName("IDMessageReaction");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__MessageRe__Messa__693CA210");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__MessageRe__React__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MessageRe__UserI__6B24EA82");
            });

            modelBuilder.Entity<MessageReport>(entity =>
            {
                entity.HasKey(e => e.IdmessageReport)
                    .HasName("PK__MessageR__3D9BEF9261A46B2F");

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
                    .HasConstraintName("FK__MessageRe__Messa__6E01572D");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__MessageRe__Sende__6EF57B66");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Idpost)
                    .HasName("PK__Post__8B0115BD53B5CEF1");

                entity.ToTable("Post");

                entity.Property(e => e.Idpost).HasColumnName("IDPost");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.PostPlace)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__UserID__71D1E811");
            });

            modelBuilder.Entity<PostReaction>(entity =>
            {
                entity.HasKey(e => e.IdpostReaction)
                    .HasName("PK__PostReac__EA97966AF0EA804C");

                entity.ToTable("PostReaction");

                entity.Property(e => e.IdpostReaction).HasColumnName("IDPostReaction");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostReact__PostI__7B5B524B");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__PostReact__React__7C4F7684");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostReact__UserI__7D439ABD");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.HasKey(e => e.IdpostReport)
                    .HasName("PK__PostRepo__852C7D3AA5043689");

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
                    .HasConstraintName("FK__PostRepor__PostI__778AC167");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__PostRepor__Sende__787EE5A0");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.HasKey(e => e.Idreaction)
                    .HasName("PK__Reaction__D866342A3F3A9EC8");

                entity.ToTable("Reaction");

                entity.Property(e => e.Idreaction).HasColumnName("IDReaction");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__User__EAE6D9DF9113F084");

                entity.ToTable("User");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Birthday).HasColumnType("date");

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

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .IsFixedLength(true);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__User__GenderID__440B1D61");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__User__LocationID__44FF419A");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK__User__UserTypeID__4316F928");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.IduserGroup)
                    .HasName("PK__UserGrou__2DE7BEFFBFDBAC58");

                entity.ToTable("UserGroup");

                entity.Property(e => e.IduserGroup).HasColumnName("IDUserGroup");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__UserGroup__Group__5629CD9C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserGroup__UserI__5535A963");
            });

            modelBuilder.Entity<UserReport>(entity =>
            {
                entity.HasKey(e => e.IduserReport)
                    .HasName("PK__UserRepo__0867783A16A131AE");

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
                    .HasConstraintName("FK__UserRepor__UserI__4F7CD00D");

                entity.HasOne(d => d.UserSender)
                    .WithMany(p => p.UserReportUserSenders)
                    .HasForeignKey(d => d.UserSenderId)
                    .HasConstraintName("FK__UserRepor__UserS__5070F446");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.IduserType)
                    .HasName("PK__UserType__EA4074F2D385167D");

                entity.ToTable("UserType");

                entity.Property(e => e.IduserType).HasColumnName("IDUserType");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Warning>(entity =>
            {
                entity.HasKey(e => e.Idwarning)
                    .HasName("PK__Warning__EECE03519874E029");

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
                    .HasConstraintName("FK__Warning__AdminID__5DCAEF64");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WarningUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Warning__UserID__5CD6CB2B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
