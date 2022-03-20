using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Thesis.Web.Models;
using Thesis.Web.ViewModels;

namespace Thesis.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyIdentityUser, MyIdentityRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Thesis.Web.Models.SubmissionDetail> SubmissionDetail { get; set; }
        public DbSet<Thesis.Web.Models.Project> Project { get; set; }
        public DbSet<Thesis.Web.Models.Subject> Subject { get; set; }
        public DbSet<Thesis.Web.Models.Faculty> Faculty { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
