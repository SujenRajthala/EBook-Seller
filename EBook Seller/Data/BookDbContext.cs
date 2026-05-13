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
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BooksCategories { get; set; }
        public DbSet<SellerBook> SellerBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(75);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(300);

            //Role
            modelBuilder.Entity<Role>().Property(r => r.RoleName).IsRequired();

            //UserRole
            modelBuilder.Entity<UserRole>().
                HasKey(ur => new { ur.RoleId, ur.UserId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            //RefreshToken
            modelBuilder.Entity<RefreshToken>().HasKey(r => r.Id);
            modelBuilder.Entity<RefreshToken>().Property(r => r.Token).HasMaxLength(200);
            modelBuilder.Entity<RefreshToken>().HasIndex(r => r.Token).IsUnique();
            modelBuilder.Entity<RefreshToken>().HasOne(r => r.User).WithMany().HasForeignKey(ur => ur.UserId);

            //Book
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>().Property(b => b.Name).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(150);
            modelBuilder.Entity<Book>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<Book>().Property(b => b.ISBN).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.ISBN).HasMaxLength(17);
            modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();

            //Category
            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Genre>().Property(g => g.Name).IsRequired();
            modelBuilder.Entity<Genre>().Property(g => g.Name).HasMaxLength(30);
            modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();

            //BookCatgory
            modelBuilder.Entity<BookGenre>().HasKey(bg => new { bg.BookId, bg.CategoryId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BooksGenres)
                .HasForeignKey(bg => bg.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(b => b.BooksGenres)
                .HasForeignKey(bg => bg.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            //SellerBook
            modelBuilder.Entity<SellerBook>().HasKey(sb => sb.Id);
            modelBuilder.Entity<SellerBook>()
                .HasOne(sb => sb.Seller)
                .WithMany(s => s.SellerBooks)
                .HasForeignKey(sb => sb.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SellerBook>()
                .HasOne(sb => sb.Book)
                .WithMany(b => b.SellerBooks)
                .HasForeignKey(sb => sb.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SellerBook>()
                .Property(sb => sb.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SellerBook>().ToTable(t =>
            {
                t.HasCheckConstraint("CK_SellerBook_Price", "[Price]>0");
                t.HasCheckConstraint("CK_SellerBook_Discount", "[Discount]>=0 AND [Discount]<=100");
            });

        }
    }
}
