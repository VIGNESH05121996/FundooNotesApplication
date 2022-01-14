// <copyright file="FundooUserContext.cs" company="Fundoo Notes Application">
//     FundooUserContext copyright tag.
// </copyright>

namespace Repository.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repository.Entities;

    /// <summary>
    /// DataBase Creation
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class FundooUserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundooUserContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public FundooUserContext(DbContextOptions options) : base(options) 
        {
        }

        /// <summary>
        /// Gets or sets the user table.
        /// </summary>
        /// <value>
        /// The user table.
        /// </value>
        public DbSet<FundooUser> UserTable { get; set; }

        /// <summary>
        /// Gets or sets the notes table.
        /// </summary>
        /// <value>
        /// The notes table.
        /// </value>
        public DbSet<FundooNotes> NotesTable { get; set; }

        /// <summary>
        /// Gets or sets the collaborator table.
        /// </summary>
        /// <value>
        /// The collaborator table.
        /// </value>
        public DbSet<FundooCollaborate> CollaboratorTable { get; set; }

        /// <summary>
        /// Gets or sets the lable table.
        /// </summary>
        /// <value>
        /// The Lable Table
        /// </value>
        public DbSet<FundooLable> LableTable { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FundooCollaborate>()
                .HasKey(e => new { e.CollaboratorId });
            modelBuilder.Entity<FundooCollaborate>()
                .HasOne(e => e.FundooNotes)
                .WithMany(e => e.FundooCollaborate)
                .HasForeignKey(e => e.NotesId);
            modelBuilder.Entity<FundooCollaborate>()
                .HasOne(e => e.FundooUser)
                .WithMany(e => e.FundooCollaborate)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<FundooLable>()
               .HasKey(e => new { e.LableId });
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