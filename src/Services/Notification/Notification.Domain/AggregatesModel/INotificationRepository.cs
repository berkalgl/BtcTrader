using Notification.Domain.SeedWork;

namespace Notification.Domain.AggregatesModel
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<Notification> AddAsync(Notification instruction);
    }
}
