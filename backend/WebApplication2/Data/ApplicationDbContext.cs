using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication2.models;

namespace WebApplication2.Data;

public class ApplicationDbContext : DbContext

{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Prona> Properties { get; set; }
    public DbSet<PropertyPhoto> PropertyPhotos { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Role> Roles { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // UserTable Config
    modelBuilder.Entity<User>()
        .HasKey(u => u.UserId);

    modelBuilder.Entity<User>()
        .Property(u => u.Email)
        .IsRequired()
        .HasMaxLength(255);

    // Ensure Email is indexed for performance and unique
    modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

    modelBuilder.Entity<User>()
        .HasMany(u => u.Properties)
        .WithOne(p => p.User)
        .HasForeignKey(p => p.UserId);

    modelBuilder.Entity<User>()
        .HasMany(u => u.Reservations)
        .WithOne(r => r.Buyer)
        .HasForeignKey(r => r.BuyerId)
        .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete here

    modelBuilder.Entity<User>()
        .HasMany(u => u.Notifications)
        .WithOne(n => n.User)
        .HasForeignKey(n => n.UserId);

    // Role Config
    modelBuilder.Entity<Role>()
        .HasKey(r => r.RoleId);

    modelBuilder.Entity<Role>()
        .Property(r => r.RoleName)
        .IsRequired()
        .HasMaxLength(50);


    modelBuilder.Entity<Role>().HasData(
        new Role { RoleId = Guid.NewGuid(), RoleName = "Admin" },
        new Role { RoleId = Guid.NewGuid(), RoleName = "User" }
    );


    // UserRole (Join Table) Config
    modelBuilder.Entity<UserRole>()
        .HasKey(ur => new { ur.UserId, ur.RoleId });

    modelBuilder.Entity<UserRole>()
        .HasOne(ur => ur.User)
        .WithMany(u => u.UserRoles)
        .HasForeignKey(ur => ur.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<UserRole>()
        .HasOne(ur => ur.Role)
        .WithMany(r => r.UserRoles)
        .HasForeignKey(ur => ur.RoleId)
        .OnDelete(DeleteBehavior.Cascade);

    // Property Config
    modelBuilder.Entity<Prona>()
        .HasKey(p => p.PropertyId);

    modelBuilder.Entity<Prona>()
        .HasMany(p => p.Photos)
        .WithOne(ph => ph.Property)
        .HasForeignKey(ph => ph.PropertyId);

    modelBuilder.Entity<Prona>()
        .HasMany(p => p.Reservations)
        .WithOne(r => r.Property)
        .HasForeignKey(r => r.PropertyId)
        .OnDelete(DeleteBehavior.Cascade);  // Allow cascade delete here

    // PropertyPhoto Config
    modelBuilder.Entity<PropertyPhoto>()
        .HasKey(pp => pp.PhotoId);

    // Reservation Config
    modelBuilder.Entity<Reservation>()
        .HasKey(r => r.ReservationId);

    modelBuilder.Entity<Reservation>()
        .Property(r => r.Status)
        .IsRequired()
        .HasMaxLength(50);

    // Notification Config
    modelBuilder.Entity<Notification>()
        .HasKey(n => n.NotificationId);
}




}