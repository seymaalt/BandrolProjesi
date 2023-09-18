using ProjeBandrol.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjeBandrol.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();


        modelBuilder.Entity<AppUser>()
        .HasMany(e => e.Vehicles)
        .WithOne(e => e.User)
        .HasForeignKey(e => e.UserId)
        .HasPrincipalKey(e => e.Id)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AppUser>()
       .HasMany(e => e.Payments)
       .WithOne(e => e.User)
       .HasForeignKey(e => e.UserId)
       .HasPrincipalKey(e => e.Id)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AppUser>()
        .HasMany(e => e.Bandrols)
        .WithOne(e => e.User)
        .HasForeignKey(e => e.UserId)
        .HasPrincipalKey(e => e.Id)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Bandrol>()
        .HasOne(e => e.Vehicle)
        .WithOne(e => e.Bandrol)
        .HasForeignKey<Bandrol>(e => e.VehicleId)
        .IsRequired();

        modelBuilder.Entity<Payment>()
        .HasOne(e => e.Vehicle)
        .WithOne(e => e.Payment)
        .HasForeignKey<Payment>(e => e.VehicleId)
        .IsRequired();


        base.OnModelCreating(modelBuilder);

    }


    public DbSet<AppUser> Users { get; set; }
    public DbSet<Bandrol> Bandrols { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<Payment> Payments { get; set; }

}
