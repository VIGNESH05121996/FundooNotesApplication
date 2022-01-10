using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Context
{
    public class FundooUserContext : DbContext
    {
        public FundooUserContext(DbContextOptions options)
           : base(options)
        {
        }
        public DbSet<FundooUser> UserTable { get; set; }
        public DbSet<FundooNotes> NotesTable { get; set; }
        public DbSet<FundooCollaborate> CollaboratorTable { get; set; }
        public DbSet<FundooLable> LableTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FundooCollaborate>()
                .HasKey(e => new {e.CollaboratorId});
            modelBuilder.Entity<FundooCollaborate>()
                .HasOne(e => e.FundooNotes)
                .WithMany(e => e.FundooCollaborate)
                .HasForeignKey(e=>e.NotesId);
            modelBuilder.Entity<FundooCollaborate>()
                .HasOne(e => e.FundooUser)
                .WithMany(e => e.FundooCollaborate)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<FundooLable>()
               .HasKey(e => new {e.LableId });
            modelBuilder.Entity<FundooLable>()
                .HasOne(e => e.FundooNotes)
                .WithMany(e => e.FundooLable)
                .HasForeignKey(e => e.NotesId);
            modelBuilder.Entity<FundooLable>()
                .HasOne(e => e.FundooUser)
                .WithMany(e => e.FundooLable)
                .HasForeignKey(e => e.UserId);
        }
    }
}

