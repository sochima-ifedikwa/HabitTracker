using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services
{
    /* 
       Team Note: We built this service to handle all the heavy lifting for Habit data. 
       It keeps our components clean and focuses on the database interactions.
    */
    public class HabitService(ApplicationDbContext context)
    {
        // We use this to grab everything a user has created, sorted nicely by name.
        public async Task<List<Habit>> GetHabitsAsync(string userId) =>
            await context.Habits
                .Where(h => h.UserId == userId)
                .OrderBy(h => h.Name)
                .ToListAsync();

        // When we need to drill down into a specific habit, we pull it by ID here.
        public async Task<Habit?> GetHabitAsync(int id, string userId) =>
            await context.Habits
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        // We added this to save new habits that our users come up with.
        public async Task<Habit> AddHabitAsync(Habit habit)
        {
            context.Habits.Add(habit);
            await context.SaveChangesAsync();
            return habit;
        }

        // We implemented this to allow users to tweak their habits later on.
        public async Task<bool> UpdateHabitAsync(Habit habit, string userId)
        {
            var existing = await context.Habits
                .FirstOrDefaultAsync(h => h.Id == habit.Id && h.UserId == userId);
            if (existing is null) return false;

            existing.Name        = habit.Name;
            existing.Description = habit.Description;
            existing.Frequency   = habit.Frequency;
            existing.Status      = habit.Status;
            await context.SaveChangesAsync();
            return true;
        }

        // Specifically for the Kanban board, we built this to just flip the status quickly.
        public async Task<bool> UpdateStatusAsync(int id, KanbanStatus status, string userId)
        {
            var habit = await context.Habits
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
            if (habit is null) return false;

            habit.Status = status;
            await context.SaveChangesAsync();
            return true;
        }

        // When a habit is no longer needed, we use this to wipe it and its logs.
        public async Task<bool> DeleteHabitAsync(int id, string userId)
        {
            var habit = await context.Habits
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
            if (habit is null) return false;

            context.Habits.Remove(habit);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
