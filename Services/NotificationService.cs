using System;

namespace HabitTracker.Services
{
    /*
       Team Note: This service is our way of talking back to the user. 
       We use it to trigger Toasts whenever something happens (like saving or deleting).
    */
    public class NotificationService
    {
        public event Action<NotificationMessage>? OnShow;

        // We use this when something goes right!
        public void ShowSuccess(string message, string title = "Success")
        {
            OnShow?.Invoke(new NotificationMessage { Message = message, Title = title, Type = NotificationType.Success });
        }

        // We use this to warn the user or show errors.
        public void ShowError(string message, string title = "Error")
        {
            OnShow?.Invoke(new NotificationMessage { Message = message, Title = title, Type = NotificationType.Error });
        }

        // Just for regularinfo updates.
        public void ShowInfo(string message, string title = "Info")
        {
            OnShow?.Invoke(new NotificationMessage { Message = message, Title = title, Type = NotificationType.Info });
        }
    }

    public class NotificationMessage
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; } = NotificationType.Info;
        public Guid Id { get; } = Guid.NewGuid();
    }

    public enum NotificationType
    {
        Success,
        Error,
        Info,
        Warning
    }
}
