using Microsoft.EntityFrameworkCore;
using Notification.Domain.SeedWork;

namespace Notification.Infrastructure
{
    public class NotificationDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "NotificationDbSchema";
        public DbSet<Domain.AggregatesModel.Notification> Notifications { get; set; }
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
