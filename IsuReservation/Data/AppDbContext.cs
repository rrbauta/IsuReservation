using IsuReservation.Abstract;
using IsuReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace IsuReservation.Data;

public class AppDbContext : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher)
        : base(options)
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactType> ContactTypes { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasOne(d => d.Destination)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasOne(d => d.ContactType)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasOne(d => d.Contact)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }

    public override int SaveChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(entity => entity.State == EntityState.Added))
        {
            item.Entity.DateCreated = now;
            item.Entity.DateModified = now;
        }

        foreach (var item in ChangeTracker.Entries<BaseEntity>()
                     .Where(entity => entity.State == EntityState.Modified))
            item.Entity.DateModified = now;

        foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(entity => entity.State == EntityState.Added))
            item.Entity.Id = Guid.NewGuid();

        try
        {
            var result = base.SaveChanges();

            return result;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(entity => entity.State == EntityState.Added))
        {
            item.Entity.DateCreated = now;
            item.Entity.DateModified = now;
        }

        foreach (var item in ChangeTracker.Entries<BaseEntity>()
                     .Where(entity => entity.State == EntityState.Modified))
            item.Entity.DateModified = now;

        foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(entity => entity.State == EntityState.Added))
            item.Entity.Id = Guid.NewGuid();

        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}