
using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Version01
{


    public class NotificationService
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
                maximumNotificationCount: MaximumNotificationCount.FromCount(3));
            });
        }

        public void ShowSuccess(string message)
        {
            notifier.ShowSuccess(message);
        }

        public void ShowWarning(string message)
        {
            notifier.ShowWarning(message);
        }

        public void ShowError(string message)
        {
            notifier.ShowError(message);
        }
    }
}
