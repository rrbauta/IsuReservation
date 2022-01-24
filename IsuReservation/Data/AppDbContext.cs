using IsuReservation.Abstract;
using IsuReservation.Models.Entities;
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
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Id = Guid.NewGuid(),
                    Name = "Destination 1",
                    Description = "Description for Destination 1",
                    Image =
                        "https://as01.epimg.net/diarioas/imagenes/2021/03/08/actualidad/1615216406_061390_1615221159_sumario_normal.jpg",
                    Rating = 5
                },
                new Destination
                {
                    Id = Guid.NewGuid(),
                    Name = "Destination 2",
                    Description = "Description for Destination 2",
                    Image =
                        "https://viajes.nationalgeographic.com.es/medio/2019/04/30/istock-840449198_07c3ef3b_1245x842.jpg",
                    Rating = 1
                },
                new Destination
                {
                    Id = Guid.NewGuid(),
                    Name = "Destination 3",
                    Description = "Description for Destination 3",
                    Image =
                        "https://viajes.nationalgeographic.com.es/medio/2019/04/30/istock-840449198_07c3ef3b_1245x842.jpg",
                    Rating = 1
                });

        modelBuilder.Entity<ContactType>()
            .HasData(
                new ContactType
                {
                    Id = Guid.NewGuid(),
                    Name = "Contact Type 1"
                },
                new ContactType
                {
                    Id = Guid.NewGuid(),
                    Name = "Contact Type 2"
                },
                new ContactType
                {
                    Id = Guid.NewGuid(),
                    Name = "Contact Type 3"
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