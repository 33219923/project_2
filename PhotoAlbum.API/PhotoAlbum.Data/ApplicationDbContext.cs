using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoAlbum.Data.Interfaces;
using PhotoAlbum.Data.Models;
using System;
using System.Linq;

namespace PhotoAlbum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<PhotoMetadata> PhotoMetadatas { get; set; }

        public virtual DbSet<SharedAlbum> SharedAlbums { get; set; }
        public virtual DbSet<SharedPhoto> SharedPhotos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Setting up vector searching using PostgresSql built in full text search
            builder.Entity<ApplicationUser>()
               .HasGeneratedTsVectorColumn(x => x.SearchVector, "english", x => new { x.Name, x.Surname })
               .HasIndex(x => x.SearchVector)
               .HasMethod("GIN");


            builder.Entity<PhotoMetadata>()
                .HasGeneratedTsVectorColumn(x => x.SearchVector, "english", x => new { x.Tags, x.Geolocation })
                .HasIndex(x => x.SearchVector)
                .HasMethod("GIN");

            //Setting composite primary keys for link tables
            builder.Entity<SharedAlbum>().HasKey(x => new { x.UserId, x.AlbumId });
            builder.Entity<SharedPhoto>().HasKey(x => new { x.UserId, x.PhotoId });
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is ICreatedTracking && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((ICreatedTracking)entityEntry.Entity).CreatedDate = DateTimeOffset.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
