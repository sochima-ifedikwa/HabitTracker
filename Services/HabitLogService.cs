using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services
{
    /*
       Team Note: This service was created to tackle the daily logging of habits. 
       We wanted a smooth way to toggle completion status without a complicated UI.
    */
    public class HabitLogService(ApplicationDbContext context)
    {
        // We check this to see if a habit has already been ticked for the day.
        public async Task<bool> IsCompletedTodayAsync(int habitId) =>
            await context.HabitLogs
                .AnyAsync(l => l.HabitId == habitId
                            && l.Date.Date == DateTime.Today
                            && l.Completed);

        // We use this to get a quick snapshot of everything the user finished today.
        public async Task<Dictionary<int, bool>> GetTodayStatusAsync(string userId)
        {
            var today = DateTime.Today;
            var logs = await context.HabitLogs
                .Where(l => l.Habit.UserId == userId
                         && l.Date.Date == today
                         && l.Completed)
                .Select(l => l.HabitId)
                .ToListAsync();
            return logs.ToDictionary(id => id, _ => true);
        }

        // We built this to be a one-click solution. If there's no log, we create it. 
        // If there is one, we just flip the 'Completed' bit.
        public async Task<bool> ToggleCompletionAsync(int habitId, string userId)
        {
            // We first make sure the habit actually belongs to the user for security.
            var habit = await context.Habits
                .FirstOrDefaultAsync(h => h.Id == habitId && h.UserId == userId);
            if (habit is null) return false;

            var today = DateTime.Today;
            var existing = await context.HabitLogs
                .FirstOrDefaultAsync(l => l.HabitId == habitId && l.Date.Date == today);

            bool newState;
            if (existing is null)
            {
                // No log yet for today? We'll start a fresh one here.
                context.HabitLogs.Add(new HabitLog
                {
                    HabitId = habitId,
                    Date = DateTime.Today,
                    Completed = true
                });
                newState = true;
            }
            else
            {
                // If it's already there, we just toggle it back and forth.
                existing.Completed = !existing.Completed;
                newState = existing.Completed;
            }

            await context.SaveChangesAsync();
            return newState;
        }
    }
}
