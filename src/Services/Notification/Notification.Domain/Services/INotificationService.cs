namespace Notification.Domain.Services
{
    public interface INotificationService
    {
        Task<AggregatesModel.Notification> AddAsync(AggregatesModel.Notification notification, CancellationToken cancellationToken);
    }
}
