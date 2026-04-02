using Microsoft.EntityFrameworkCore;
using HabitTracker.Models;

namespace HabitTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitLog> HabitLogs { get; set; }
    }
}