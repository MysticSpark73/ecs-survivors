using Code.Gameplay.Features.Statuses.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Statuses
{
    public sealed class StatusFeature : Feature
    {
        public StatusFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<StatusDurationSystem>());
            Add(systemFactory.Create<DamageOverTimeStatusSystem>());
            Add(systemFactory.Create<StatusVisualsFeature>());
            Add(systemFactory.Create<CleanupUnappliedStatusesSystem>());
        }
    }
}