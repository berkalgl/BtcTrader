using Notification.Domain.AggregatesModel;

namespace Notification.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<AggregatesModel.Notification> AddAsync(AggregatesModel.Notification notification, CancellationToken cancellationToken)
        {
            await _notificationRepository.AddAsync(notification);
            await _notificationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return notification;
        }
    }
}
