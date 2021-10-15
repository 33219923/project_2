using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoAlbum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Setting up vector searching using PostgresSql built in full text search
            builder.Entity<Photo>()
                .HasGeneratedTsVectorColumn(p => p.SearchVector, "english", p => new { }) //TODO: Update the searchable columns
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");

            //Example: Searching on the vector 
            //var npgsql = context.Products.Where(p => p.SearchVector.Matches("Npgsql")).ToList();
        }

        public virtual DbSet<Photo> Photos { get; set; }
    }
}
