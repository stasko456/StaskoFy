using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaskoFy.Models.Entities;

namespace StaskoFy.DataAccess
{
    public class StaskoFyDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public StaskoFyDbContext(DbContextOptions<StaskoFyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<User>()
            //    .Property(u => u.UserName)
            //    .HasMaxLength(20)
            //    .IsRequired();

            //builder.Entity<User>()
            //    .Property(u => u.Email)
            //    .HasMaxLength(60)
            //    .IsRequired();
        }
    }
}
