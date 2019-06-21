using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace Logsheet.Persistence
{
    public class LogsheetDbContext : DbContext
    {
        public LogsheetDbContext(DbContextOptions options) : base(options)
        {
        }

//        public DbSet<Identity> Identities { get; set; }
//
//        public DbSet<NotificationType> NotificationTypes { get; set; }
//
//        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("logs");

            modelBuilder.ProtectSensitiveInformation();

//            modelBuilder.Entity<Session>()
//                .ToTable("Sessions");

//            modelBuilder.Entity<Identity>()
//                .HasOne(i => i.Session)
//                .WithOne(s => s.Identity)
//                .HasForeignKey<Session>("IdentityId");
//
//            modelBuilder.Entity<NotificationTypeChannel>()
//                .ToTable("NotificationTypeChannels");
//
//            modelBuilder.Entity<NotificationChannelTemplate>()
//                .ToTable("NotificationChannelTemplates")
//                .HasDiscriminator<string>("Type")
//                .HasValue<EmailChannelTemplate>(ChannelTemplateTypes.Email);
//
//            modelBuilder.Entity<EmailChannelTemplate>()
//                .HasBaseType<NotificationChannelTemplate>();
//
//            modelBuilder.Entity<NotificationTypeChannel>()
//                .HasOne(i => i.NotificationChannelTemplate)
//                .WithOne(s => s.NotificationTypeChannel)
//                .HasForeignKey<NotificationChannelTemplate>("NotificationTypeChannelId");
//
//            modelBuilder.Entity<User>()
//                .ToTable("Users");
        }
    }
}
