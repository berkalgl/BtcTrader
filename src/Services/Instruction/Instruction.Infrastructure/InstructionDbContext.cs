using Instruction.Domain.AggregatesModel;
using Instruction.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Instruction.Infrastructure
{
    public class InstructionDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "InstructionDbSchema";
        public DbSet<Domain.AggregatesModel.Instruction> Instructions { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Price> Prices { get; set; }

        private readonly IMediator _mediator;
        public InstructionDbContext(DbContextOptions<InstructionDbContext> options) : base(options) { }
        public InstructionDbContext(DbContextOptions<InstructionDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>()
                .HasData(Enumeration.GetAll<Status>());

            modelBuilder.Entity<Price>()
                .HasNoKey();

            modelBuilder.Ignore<Price>();
            modelBuilder.Entity<Domain.AggregatesModel.Instruction>()
                .OwnsOne(i => i.Price, p =>
                {
                    p.Property<int>("InstructionId");
                    p.WithOwner();
                });
        }
    }
    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return default;
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return default;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<TResponse>(default);
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(object));
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            return Task.CompletedTask;
        }
    }
}
