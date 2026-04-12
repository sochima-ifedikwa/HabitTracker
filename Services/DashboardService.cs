using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services
{
    public record HabitProgress(Habit Habit, double WeeklyPercent, bool CompletedToday);

    public record DashboardStats(
        int TotalHabits,
        int CompletedToday,
        int CurrentStreak,
        List<HabitProgress> HabitProgress);

    /*
       Team Note: This service calculates the big picture stats for the user. 
       We handle logic like streaks and weekly percentages so the UI can just display it.
    */
    public class DashboardService(ApplicationDbContext context)
    {
        // We gather everything together here: total counts, streaks, and progress for the last 7 days.
        public async Task<DashboardStats> GetStatsAsync(string userId)
        {
            var today = DateTime.Today;
            var weekAgo = today.AddDays(-6);

            var habits = await context.Habits
                .Where(h => h.UserId == userId)
                .ToListAsync();

            if (habits.Count == 0)
                return new DashboardStats(0, 0, 0, []);

            var habitIds = habits.Select(h => h.Id).ToList();

            var logs = await context.HabitLogs
                .Where(l => habitIds.Contains(l.HabitId)
                         && l.Date >= weekAgo
                         && l.Completed)
                .ToListAsync();

            var completedToday = logs
                .Where(l => l.Date.Date == today)
                .Select(l => l.HabitId)
                .Distinct()
                .Count();

            // Streak logic: we count backwards from today to see how many consecutive days 
            // the user has been active on any of their habits.
            var completedDays = await context.HabitLogs
                .Where(l => habitIds.Contains(l.HabitId) && l.Completed)
                .Select(l => l.Date.Date)
                .Distinct()
                .ToListAsync();

            int streak = 0;
            var checkDay = today;
            while (completedDays.Contains(checkDay))
            {
                streak++;
                checkDay = checkDay.AddDays(-1);
            }

            // We calculate the individual progress for each habit here.
            var progress = habits.Select(h =>
            {
                var habitLogs = logs.Where(l => l.HabitId == h.Id).ToList();
                var divisor = h.Frequency == HabitFrequency.Daily ? 7.0 : 1.0;
                var completedDaysCount = habitLogs.Select(l => l.Date.Date).Distinct().Count();
                var pct = Math.Min(100, completedDaysCount / divisor * 100);
                var doneToday = habitLogs.Any(l => l.Date.Date == today);
                return new HabitProgress(h, Math.Round(pct, 0), doneToday);
            }).ToList();

            return new DashboardStats(habits.Count, completedToday, streak, progress);
        }
    }
}
