using BandrolSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjeBandrol.Models;

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



          
    }


    public DbSet<AppUser> Users { get; set; }
    public DbSet<Bandrol> Bandrols { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
}
