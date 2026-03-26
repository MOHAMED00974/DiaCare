using DiaCare.Domain.Entities;
using DiaCare.Domain.Interfaces;
using DiaCare.Infrastructure.Data;

namespace DiaCare.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

 
        public IBaseRepository<HealthProfile> HealthProfiles { get; private set; }
        public IBaseRepository<PredictionResult> PredictionResults { get; private set; }
        public IBaseRepository<Article> Articles { get; private set; }
        public IBaseRepository<Notification> Notifications { get; private set; }
        public IBaseRepository<ChatMessage> ChatMessages { get; private set; }
        public IBaseRepository<Badge> Badges { get; private set; }
        public IBaseRepository<UserBadge> UserBadges { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

         
            HealthProfiles = new BaseRepository<HealthProfile>(_context);
            PredictionResults = new BaseRepository<PredictionResult>(_context);
            Articles = new BaseRepository<Article>(_context);
            Notifications = new BaseRepository<Notification>(_context);
            ChatMessages = new BaseRepository<ChatMessage>(_context);
            Badges = new BaseRepository<Badge>(_context);
            UserBadges = new BaseRepository<UserBadge>(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}