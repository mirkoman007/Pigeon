using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
                optionsBuilder.UseSqlServer("Server=den1.mssql8.gear.host;Database=pigeondb3;User Id=pigeondb3;Password=Py42ssQ!?3jz;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Croatian_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Idcity)
                    .HasName("PK__City__36D3508359847F2F");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__City__CountryID__38996AB5");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Idcomment)
                    .HasName("PK__Comment__C71D7B98AB93D2DC");

                entity.Property(e => e.Text).IsUnicode(false);

                entity.HasOne(d => d.CommentNavigation)
                    .WithMany(p => p.InverseCommentNavigation)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__Comment__Comment__02084FDA");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Comment__PostID__01142BA1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__UserID__02FC7413");
            });

            modelBuilder.Entity<CommentReaction>(entity =>
            {
                entity.HasKey(e => e.IdcommentReaction)
                    .HasName("PK__CommentR__1DB6A5903F081B8B");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__CommentRe__Comme__05D8E0BE");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__CommentRe__React__06CD04F7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__CommentRe__UserI__07C12930");
            });

            modelBuilder.Entity<CommentReport>(entity =>
            {
                entity.HasKey(e => e.IdcommentReport)
                    .HasName("PK__CommentR__3027E215F3C96DE6");

                entity.Property(e => e.Explanation).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__CommentRe__Comme__0A9D95DB");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__CommentRe__Sende__0B91BA14");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Idcountry)
                    .HasName("PK__Country__D9D5A69476120169");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<DeletedMessage>(entity =>
            {
                entity.HasKey(e => e.IddeletedMessage)
                    .HasName("PK__DeletedM__5D8807BA8D28E788");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.DeletedMessages)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__DeletedMe__Messa__6477ECF3");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.Idfriend)
                    .HasName("PK__Friend__FB73C39026ABA11F");

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
                    .HasName("PK__FriendRe__FC78D63CD27E05D7");

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
                    .HasName("PK__Gender__42370E7975E73C4F");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Idgroup)
                    .HasName("PK__Group__CB4260CAE570A18D");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<GroupReport>(entity =>
            {
                entity.HasKey(e => e.IdgroupReport)
                    .HasName("PK__GroupRep__0A30267B31A3E46D");

                entity.Property(e => e.Explanation).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

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
                    .HasName("PK__Location__C2B752778613DC46");

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
                    .HasName("PK__Media__F2B436C5F0CCC5FB");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Media__PostID__75A278F5");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Idmessage)
                    .HasName("PK__Message__195595EC27335381");

                entity.Property(e => e.Text).IsUnicode(false);

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
                    .HasName("PK__MessageR__0AD165E72322A4B9");

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
                    .HasName("PK__MessageR__3D9BEF923D569F76");

                entity.Property(e => e.Explanation).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

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
                    .HasName("PK__Post__8B0115BD555A44BB");

                entity.Property(e => e.Text).IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Post__GroupID__71D1E811");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__UserID__72C60C4A");
            });

            modelBuilder.Entity<PostReaction>(entity =>
            {
                entity.HasKey(e => e.IdpostReaction)
                    .HasName("PK__PostReac__EA97966AF31C0848");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostReact__PostI__7C4F7684");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("FK__PostReact__React__7D439ABD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostReact__UserI__7E37BEF6");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.HasKey(e => e.IdpostReport)
                    .HasName("PK__PostRepo__852C7D3ADD0175EE");

                entity.Property(e => e.Explanation).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostRepor__PostI__787EE5A0");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__PostRepor__Sende__797309D9");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.HasKey(e => e.Idreaction)
                    .HasName("PK__Reaction__D866342A8CEFF008");

                entity.Property(e => e.Value).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__User__EAE6D9DFE6EB9EF6");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsFixedLength(true);

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
                    .HasName("PK__UserGrou__2DE7BEFF58D1715A");

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
                    .HasName("PK__UserRepo__0867783A4DBB52C1");

                entity.Property(e => e.Explanation).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

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
                    .HasName("PK__UserType__EA4074F226A56A11");

                entity.Property(e => e.Value).IsUnicode(false);
            });

            modelBuilder.Entity<Warning>(entity =>
            {
                entity.HasKey(e => e.Idwarning)
                    .HasName("PK__Warning__EECE03516AE1FE2E");

                entity.Property(e => e.Explanation).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

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
