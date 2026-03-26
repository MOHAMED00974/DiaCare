using DiaCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaCare.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Repositories reg for all tabels
        IBaseRepository<HealthProfile> HealthProfiles { get; }
        IBaseRepository<PredictionResult> PredictionResults { get; }
        IBaseRepository<Article> Articles { get; }
        IBaseRepository<Notification> Notifications { get; }
        IBaseRepository<ChatMessage> ChatMessages { get; }
        IBaseRepository<Badge> Badges { get; }
        IBaseRepository<UserBadge> UserBadges { get; }

        // save in database all at once
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
