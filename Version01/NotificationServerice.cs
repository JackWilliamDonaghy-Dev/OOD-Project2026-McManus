/*
using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace Version01
{
    internal class NotificationService
    {
        private Notifier notifier;

        public NotificationService(Window window)
        {
            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: window,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: 5);
            });
        }

        public void ShowSuccess(string message)
        {
            notifier.Notify(() => new NotificationContent
            {
                Title = "Success",
                Message = message,
                Type = NotificationType.Success
            });
        }

        public void ShowWarning(string message)
        {
            notifier.Notify(() => new NotificationContent
            {
                Title = "Warning",
                Message = message,
                Type = NotificationType.Warning
            });
        }

        public void ShowError(string message)
        {
            notifier.Notify(() => new NotificationContent
            {
                Title = "Error",
                Message = message,
                Type = NotificationType.Error
            });
        }
    }
}
*/