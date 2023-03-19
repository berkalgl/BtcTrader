using Notification.Domain.AggregatesModel;
using Notification.Domain.SeedWork;

namespace Notification.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;
        public NotificationRepository(NotificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.AggregatesModel.Notification> AddAsync(Domain.AggregatesModel.Notification notification)
        {
            var added = await _dbContext.Notifications.AddAsync(notification);
            return added.Entity;
        }
    }
}
