using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services
{
    public class HabitService
    {
        private readonly ApplicationDbContext _context;

        public HabitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Habit>> GetHabits()
        {
            return await _context.Habits.ToListAsync();
        }

        public async Task AddHabit(Habit habit)
        {
            _context.Habits.Add(habit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHabit(int id)
        {
            var habit = await _context.Habits.FindAsync(id);
            if (habit != null)
            {
                _context.Habits.Remove(habit);
                await _context.SaveChangesAsync();
            }
        }
    }
}