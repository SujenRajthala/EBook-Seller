using EBook_Seller.Models;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data
{
    public class BookDbContext(DbContextOptions<BookDbContext> options):DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(75);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(300);

            modelBuilder.Entity<Role>().Property(r => r.RoleName).IsRequired();

            modelBuilder.Entity<UserRole>().
                HasKey(ur => new { ur.RoleId, ur.UserId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u=>u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>().HasKey(r => r.Id);
            modelBuilder.Entity<RefreshToken>().Property(r => r.Token).HasMaxLength(200);
            modelBuilder.Entity<RefreshToken>().HasIndex(r => r.Token).IsUnique();
            modelBuilder.Entity<RefreshToken>().HasOne(r => r.User).WithMany().HasForeignKey(ur => ur.UserId);
            
        }
    }
}
