using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Identity.Client;
using NET.Kniaz.ProperArchitecture.Domain.Entities;

namespace NET.Kniaz.ProperArchitecture.Persistence
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<Sprint> Sprints { get; set; }

        public DbSet<Story> Stories { get; set; }

        public DbSet<Commit> Commits { get; set; }

        public DbSet<SimulationResult> SimulationResults { get; set; }

    }
}

