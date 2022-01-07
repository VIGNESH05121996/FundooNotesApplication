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
    }
}

