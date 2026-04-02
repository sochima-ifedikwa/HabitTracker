using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class Habit
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}